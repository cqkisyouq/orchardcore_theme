using FlyingRat.Module.AttachContent.Models;
using FlyingRat.Module.AttachContent.Settings;
using FlyingRat.Module.AttachContent.ViewModel;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Liquid;
using OrchardCore.Liquid.Drivers;
using OrchardCore.Mvc.ModelBinding;
using System.Linq;
using System.Threading.Tasks;

namespace FlyingRat.Module.AttachContent.Drivers
{
    public class AttachContentPartDisplayDriver : ContentPartDisplayDriver<AttachContentPart>
    {
        private readonly ILiquidTemplateManager _liquidTemplatemanager;
        private readonly IStringLocalizer S;
        private readonly IContentDefinitionManager _contentDefinitionManager;
        public AttachContentPartDisplayDriver(
            IContentDefinitionManager contentDefinitionManager,
            ILiquidTemplateManager liquidTemplatemanager,
            IStringLocalizer<LiquidPartDisplayDriver> localizer
            )
        {
            _contentDefinitionManager = contentDefinitionManager;
            S = localizer;
            _liquidTemplatemanager = liquidTemplatemanager;
        }

        public override IDisplayResult Display(AttachContentPart part)
        {
            return Combine(
               Initialize<AttachContentPartViewModel>("AttachContentPart", m => BuildViewModel(m, part))
                   .Location("Detail", "Content:20"),
               Initialize<AttachContentPartViewModel>("AttachContentPart_Summary", m => BuildViewModel(m, part))
                   .Location("Meta", "Content:5")
           );
        }

        public override IDisplayResult Edit(AttachContentPart part)
        {
            return Initialize<AttachContentPartViewModel>("AttachContentPart_Edit", m => BuildViewModel(m, part));
        }

        public override async Task<IDisplayResult> UpdateAsync(AttachContentPart model, IUpdateModel updater)
        {
            var viewModel = new AttachContentPartViewModel();
            if (await updater.TryUpdateModelAsync(viewModel, Prefix, t => t.HideContent, t => t.AttachContent))
            {
                if (!string.IsNullOrEmpty(viewModel.AttachContent) && !_liquidTemplatemanager.Validate(viewModel.AttachContent, out var errors))
                {
                    updater.ModelState.AddModelError(Prefix, nameof(viewModel.AttachContent), S["The Liquid Body doesn't contain a valid Liquid expression. Details: {0}", string.Join(" ", errors)]);
                }
                else
                {
                    model.AttachContent = viewModel.AttachContent;
                }
                model.HideContent = viewModel.HideContent;
            }

            return Edit(model);
        }
        public AttachContentPartSettings GetAttachContentPartSettings(AttachContentPart part)
        {
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(part.ContentItem.ContentType);
            var contentTypePartDefinition = contentTypeDefinition.Parts.FirstOrDefault(p => p.PartDefinition.Name == nameof(AttachContentPart));
            var settings = contentTypePartDefinition.GetSettings<AttachContentPartSettings>();

            return settings;
        }
        private void BuildViewModel(AttachContentPartViewModel model, AttachContentPart part)
        {
            var settings = GetAttachContentPartSettings(part);

            model.ContentItem = part.ContentItem;
            model.ShortName = settings.ShortName;
            model.HideContent = part.HideContent;
            model.AttachContent = part.AttachContent;
            model.AttachContentPart = part;
            model.Settings = settings;
        }
    }
}
