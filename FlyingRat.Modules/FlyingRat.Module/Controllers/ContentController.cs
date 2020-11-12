using GraphQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Lists.Models;
using OrchardCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YesSql;

namespace FlyingRat.Module.Controllers
{
    public class ContentController: Controller
    {
        private readonly IContentManager _contentManager;
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IContentItemDisplayManager _contentItemDisplayManager;
        private readonly dynamic New;
        private readonly INotifier _notifier;
        private readonly ISession _session;
        private readonly IHtmlLocalizer H;
        private readonly IUpdateModelAccessor _updateModelAccessor;
        public ContentController(
            IContentManager contentManager,
            IContentDefinitionManager contentDefinitionManager,
            IContentItemDisplayManager contentItemDisplayManager,
            IShapeFactory shapeFactory,
            INotifier notifier,
            ISession session,
            IUpdateModelAccessor updateModelAccessor,
            IHtmlLocalizer<ContentController> htmlLocalizer
            )
        {
            _contentDefinitionManager = contentDefinitionManager;
            _contentManager = contentManager;
            _contentItemDisplayManager = contentItemDisplayManager;
            New = shapeFactory;
            _notifier = notifier;
            _session = session;
            _updateModelAccessor = updateModelAccessor;
            H = htmlLocalizer;
        }

        public async ValueTask<IActionResult> Create(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var contentItem =await _contentManager.NewAsync(id);
            var model = await _contentItemDisplayManager.BuildEditorAsync(contentItem, _updateModelAccessor.ModelUpdater, true);
            return View(model);
        }

        [HttpPost,ActionName("Create")]
        [FormValueRequired("submit.Publish")]
        public async ValueTask<IActionResult> CreatePost(string id, string returnUrl)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var contentItem = await _contentManager.NewAsync(id);
            contentItem.Alter<ContainedPart>(x =>
            {
                x.ListContentItemId = "4xzz7rfkbm5afsntx0bj4fad5q";
            });
            contentItem.Owner = User.Identity.Name;
            var model = await _contentItemDisplayManager.UpdateEditorAsync(contentItem, _updateModelAccessor.ModelUpdater, true);
            if (!ModelState.IsValid)
            {
                _session.Cancel();
                return View("Create",model);
            }
            await _contentManager.CreateAsync(contentItem, VersionOptions.Draft);
            await _contentManager.PublishAsync(contentItem);
            _notifier.Success(H["发布成功"]);
            if ((!string.IsNullOrEmpty(returnUrl)))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToRoute(returnUrl);
        }
    }
}
