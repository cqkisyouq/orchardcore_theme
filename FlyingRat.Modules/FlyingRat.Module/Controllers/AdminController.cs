using FlyingRat.Module.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlyingRat.Module.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDisplayManager<ISite> _siteSettingsDisplayManager;
        private readonly ISiteService _siteService;
        private readonly INotifier _notifier;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUpdateModelAccessor _updateModelAccessor;
        private readonly IHtmlLocalizer H;
        public AdminController(
            ISiteService siteService,
            IDisplayManager<ISite> siteSettingsDisplayManager,
            IAuthorizationService authorizationService,
            INotifier notifier,
            IHtmlLocalizer<AdminController> h,
            IUpdateModelAccessor updateModelAccessor
            )
        {
            _siteSettingsDisplayManager = siteSettingsDisplayManager;
            _siteService = siteService;
            _notifier = notifier;
            _authorizationService = authorizationService;
            _updateModelAccessor = updateModelAccessor;
            H = h;
        }

        public async ValueTask<IActionResult> Index(string groupId)
        {
            var site = await _siteService.GetSiteSettingsAsync();
            var shape = await _siteSettingsDisplayManager.BuildEditorAsync(site, _updateModelAccessor.ModelUpdater, false, groupId);
            var model = new AdminIndexViewModel()
            {
                GroupId=groupId,
                Shape = shape
            };
            return View(model);
        }

        [HttpPost]
        [ActionName(nameof(Index))]
        public async ValueTask<IActionResult> IndexPost(string groupId)
        {
            var site = await _siteService.LoadSiteSettingsAsync();

            var viewModel = new AdminIndexViewModel
            {
                GroupId = groupId,
                Shape = await _siteSettingsDisplayManager.UpdateEditorAsync(site, _updateModelAccessor.ModelUpdater, false, groupId)
            };

            if (ModelState.IsValid)
            {
                await _siteService.UpdateSiteSettingsAsync(site);

                _notifier.Success(H["Site settings updated successfully."]);

                return RedirectToAction(nameof(Index), new { groupId });
            }

            return View(viewModel);
        }
    }
}
