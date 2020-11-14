using FlyingRat.Module.Setting.Models;
using FlyingRat.Module.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Records;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Entities;
using OrchardCore.Lists.Models;
using OrchardCore.Routing;
using OrchardCore.Settings;
using System.Linq;
using System.Threading.Tasks;
using YesSql;
using YesSql.Services;

namespace FlyingRat.Module.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        private readonly IContentManager _contentManager;
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IContentItemDisplayManager _contentItemDisplayManager;
        private readonly dynamic New;
        private readonly INotifier _notifier;
        private readonly ISession _session;
        private readonly IHtmlLocalizer H;
        private readonly IUpdateModelAccessor _updateModelAccessor;
        private readonly ISiteService _siteService;
        private const string PublishContent = "BlogPost";
        public ContentController(
            IContentManager contentManager,
            IContentDefinitionManager contentDefinitionManager,
            IContentItemDisplayManager contentItemDisplayManager,
            IShapeFactory shapeFactory,
            INotifier notifier,
            ISession session,
            IUpdateModelAccessor updateModelAccessor,
            IHtmlLocalizer<ContentController> htmlLocalizer,
            ISiteService siteService
            )
        {
            _contentDefinitionManager = contentDefinitionManager;
            _contentManager = contentManager;
            _contentItemDisplayManager = contentItemDisplayManager;
            New = shapeFactory;
            _notifier = notifier;
            _session = session;
            _updateModelAccessor = updateModelAccessor;
            _siteService = siteService;
            H = htmlLocalizer;
        }

        public async ValueTask<IActionResult> Index()
        {
            var site = await _siteService.LoadSiteSettingsAsync();
            var publish = site.As<PublishSetting>();
            var items = await _session.Query<ContentItem>().With<ContentItemIndex>(x => x.ContentItemId.IsIn(publish.ListContentItemIds) && x.Published).ListAsync();
            return View(items);
        }

        public async ValueTask<IActionResult> Create(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var validate = (await _siteService.LoadSiteSettingsAsync())?.As<PublishSetting>()?.ListContentItemIds.Contains(id) ?? false;
            if (!validate) return NotFound();

            var contentItem = await _contentManager.NewAsync(PublishContent);
            var shape = await _contentItemDisplayManager.UpdateEditorAsync(contentItem, _updateModelAccessor.ModelUpdater, true);
            var model = new PublishContentViewModel()
            {
                TargetId = id,
                Shape= shape
            };
            return View(model);
        }

        [HttpPost, ActionName("Create")]
        [FormValueRequired("submit.Publish")]
        public async ValueTask<IActionResult> CreatePost(PublishContentViewModel model)
        {
            if (string.IsNullOrEmpty(model?.TargetId)) return NotFound();
            var validate = (await _siteService.LoadSiteSettingsAsync())?.As<PublishSetting>()?.ListContentItemIds.Contains(model.TargetId) ?? false;
            if (!validate) return NotFound();

            var contentItem = await _contentManager.NewAsync(PublishContent);
            contentItem.Alter<ContainedPart>(x =>
            {
                x.ListContentItemId = model.TargetId;
            });
            contentItem.Owner = User.Identity.Name;

            var shape = await _contentItemDisplayManager.UpdateEditorAsync(contentItem, _updateModelAccessor.ModelUpdater, true);
            if (!ModelState.IsValid)
            {
                _session.Cancel();
                return View("Create", model);
            }
            await _contentManager.CreateAsync(contentItem, VersionOptions.Draft);
            await _contentManager.PublishAsync(contentItem);
            _notifier.Success(H["发布成功"]);
            return RedirectToAction("index");
        }
    }
}
