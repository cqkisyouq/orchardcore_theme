using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Records;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Lists.Drivers;
using OrchardCore.Lists.Indexes;
using OrchardCore.Lists.Models;
using OrchardCore.Lists.Services;
using OrchardCore.Lists.ViewModels;
using OrchardCore.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;
using YesSql;

namespace FlyingRat.Module.Drivers
{
    public class BraksnListPartDriver : ListPartDisplayDriver
    {
        private readonly ISession _session;
        private readonly IContainerService _containerService;
        private readonly IContentDefinitionManager _contentDefinitionManager;
        public BraksnListPartDriver(
            ISession session,
            IContentDefinitionManager contentDefinitionManager,
            IContainerService containerService
            ) : base(contentDefinitionManager, containerService)
        {
            _session = session;
            _containerService = containerService;
            _contentDefinitionManager = contentDefinitionManager;
        }

        public override IDisplayResult Display(ListPart part, BuildPartDisplayContext context)
        {
            return Combine(
                Initialize<ListPartViewModel>(GetDisplayShapeType(context),async model =>
                {
                   await BuildViewModel(context, part, model);
                })
                    .Location("Detail", "Content:10"),
                Initialize<ListPartViewModel>("ListPart",async model =>
                {
                   await BuildViewModel(context, part, model, false);
                })
                    .Location("DetailAdmin", "Content:10")
                );
        }

        private async ValueTask BuildViewModel(BuildPartDisplayContext context, ListPart part, ListPartViewModel model,bool published=true)
        {
            var settings = context.TypePartDefinition.GetSettings<ListPartSettings>();
            var pager = new PagerParameters() { Page = 1, PageSize = settings.PageSize };
            await context.Updater.TryUpdateModelAsync<PagerParameters>(pager);
            var count = (pager.Page - 1) * pager.PageSize;
            var query = _session.Query<ContentItem, ContainedPartIndex>(x => x.ListContentItemId == part.ContentItem.ContentItemId);
            if (settings.EnableOrdering) query.OrderBy(x => x.Order);
            var searchQuery=query.With<ContentItemIndex>();
            if (published)
            {
                searchQuery.Where(x => x.Published);
            }
            else
            {
                searchQuery.Where(x => x.Latest);
            }

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
    }
}
