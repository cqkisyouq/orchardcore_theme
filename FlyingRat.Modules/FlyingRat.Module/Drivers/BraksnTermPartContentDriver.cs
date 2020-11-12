using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Records;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Navigation;
using OrchardCore.Settings;
using OrchardCore.Taxonomies.Drivers;
using OrchardCore.Taxonomies.Indexing;
using OrchardCore.Taxonomies.Models;
using OrchardCore.Taxonomies.ViewModels;
using System;
using System.Threading.Tasks;
using YesSql;

namespace FlyingRat.Module.Drivers
{

    public class BraksnTermPartContentDriver : TermPartContentDriver
    {
        private readonly ISession _session;
        private readonly ISiteService _siteService;
        private readonly IContentManager _contentManager;
        public BraksnTermPartContentDriver(
            ISession session,
            IContentManager contentManager,
            ISiteService siteService
            ):base(session,siteService,contentManager)
        {
            _session = session;
            _siteService = siteService;
            _contentManager = contentManager;
        }

        public override Task<IDisplayResult> DisplayAsync(ContentItem contentItem, BuildDisplayContext context)
        {
            var part = contentItem.As<TermPart>();
            if(part==null) return Task.FromResult<IDisplayResult>(null);

            return Task.FromResult<IDisplayResult>(
                Initialize<TermPartViewModel>("TermPart", async model =>
                {
                    var settings = await _siteService.GetSiteSettingsAsync();
                    var pager = new PagerParameters() { Page = 1, PageSize = settings.PageSize };
                    await context.Updater.TryUpdateModelAsync<PagerParameters>(pager);
                    var count = (Math.Max(0, pager.Page.Value - 1)) * pager.PageSize.Value;
                    var query = _session.Query<ContentItem>()
                            .With<TaxonomyIndex>(x => x.TermContentItemId == part.ContentItem.ContentItemId
                                    && x.ContentType == "BlogPost")
                            .With<ContentItemIndex>(x=>x.Published)
                            .OrderByDescending(x => x.CreatedUtc);

                    var totalCount = await query.CountAsync();
                    var items = await query.Skip(Math.Max(0, count)).Take(pager.PageSize.Value).ListAsync();

                    model.TaxonomyContentItemId = part.TaxonomyContentItemId;
                    model.ContentItem = part.ContentItem;
                    model.ContentItems = await _contentManager.LoadAsync(items);
                    model.Pager =(await context.New.Pager(pager)).TotalItemCount(totalCount);
                    
                }).Location("Detail", "Content:5")
                );

        }
    }
}
