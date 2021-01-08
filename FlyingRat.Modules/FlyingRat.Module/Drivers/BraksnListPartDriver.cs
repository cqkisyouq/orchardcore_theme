using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Records;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Lists.Drivers;
using OrchardCore.Lists.Indexes;
using OrchardCore.Lists.Models;
using OrchardCore.Lists.Services;
using OrchardCore.Lists.ViewModels;
using OrchardCore.Navigation;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YesSql;
using HttpContext = Microsoft.AspNetCore.Http.IHttpContextAccessor;

namespace FlyingRat.Module.Drivers
{
    public class BraksnListPartDriver : ListPartDisplayDriver
    {
        private readonly ISession _session;
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IUpdateModelAccessor _updateModelAccessor;
        private readonly HttpContext _httpContext;
        public BraksnListPartDriver(
            ISession session,
            IContentDefinitionManager contentDefinitionManager,
            IUpdateModelAccessor updateModelAccessor,
            IContainerService containerService,
            HttpContext httpContext
            ) : base(contentDefinitionManager, containerService, updateModelAccessor)
        {
            _session = session;
            _contentDefinitionManager = contentDefinitionManager;
            _updateModelAccessor = updateModelAccessor;
            _httpContext = httpContext;
        }

        public override IDisplayResult Display(ListPart part, BuildPartDisplayContext context)
        {
            return Combine(
                Initialize<ListPartViewModel>(GetDisplayShapeType(context),async model =>
                {
                   await BuildViewModel(context, part, model,new ContainedItemOptions());
                })
                    .Location("Detail", "Content:10"),
                Initialize<ListPartViewModel>("ListPart",async model =>
                {
                    var listPartFilterViewModel = new ListPartFilterViewModel();
                    var containedItemOptions = new ContainedItemOptions();
                    await _updateModelAccessor.ModelUpdater.TryUpdateModelAsync(listPartFilterViewModel, Prefix);
                    containedItemOptions.DisplayText = listPartFilterViewModel.DisplayText;
                    containedItemOptions.Status = listPartFilterViewModel.Status;
                    model.ListPartFilterViewModel = listPartFilterViewModel;
                    await BuildViewModel(context, part, model, containedItemOptions);
                })
                    .Location("DetailAdmin", "Content:10")
                );
        }

        private async ValueTask BuildViewModel(BuildPartDisplayContext context, ListPart part, ListPartViewModel model, ContainedItemOptions options)
        {
            var settings = context.TypePartDefinition.GetSettings<ListPartSettings>();
            var pager = new PagerParameters() { Page = 1, PageSize = settings.PageSize };
            await context.Updater.TryUpdateModelAsync<PagerParameters>(pager); 
             var count = (pager.Page - 1) * pager.PageSize;
            var query = _session.Query<ContentItem, ContainedPartIndex>(x => x.ListContentItemId == part.ContentItem.ContentItemId);
            if (settings.EnableOrdering) query.OrderBy(x => x.Order);
            var searchQuery=query.With<ContentItemIndex>();
            ApplyContainedItemOptionsFilter(options, searchQuery);
            if (settings.EnableOrdering == false) searchQuery.OrderByDescending(x => x.CreatedUtc);

            var totalCount = await searchQuery.CountAsync();
            var items = await searchQuery.Skip(Math.Max(0, count.Value)).Take(pager.PageSize.Value).ListAsync();
            var contentTypes = settings.ContainedContentTypes ?? Enumerable.Empty<string>();
            model.ContainedContentTypeDefinitions = contentTypes.Select(contentType => _contentDefinitionManager.GetTypeDefinition(contentType));
            model.ContentItems = items;
            model.ListPart = part;
            model.Context = context;
            model.EnableOrdering = settings.EnableOrdering;
            model.Pager = (await context.New.Pager(pager)).TotalItemCount(totalCount);
        }

        private void ApplyContainedItemOptionsFilter(ContainedItemOptions containedItemOptions, IQuery<ContentItem> query)
        {
            if (!string.IsNullOrEmpty(containedItemOptions.DisplayText))
            {
                query.With<ContentItemIndex>(i => i.DisplayText.Contains(containedItemOptions.DisplayText));
            }

            switch (containedItemOptions.Status)
            {
                case ContentsStatus.Published:
                    query.With<ContentItemIndex>(i => i.Published);
                    break;
                case ContentsStatus.Latest:
                    query.With<ContentItemIndex>(i => i.Latest);
                    break;
                case ContentsStatus.Draft:
                    query.With<ContentItemIndex>(i => !i.Published && i.Latest);
                    break;
                case ContentsStatus.Owner:
                    var currentUserName = _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (currentUserName != null)
                    {
                        query.With<ContentItemIndex>(i => i.Owner == currentUserName);
                    }
                    break;
                default:
                    throw new NotSupportedException("Unknown status filter.");
            }
        }
    }
}
