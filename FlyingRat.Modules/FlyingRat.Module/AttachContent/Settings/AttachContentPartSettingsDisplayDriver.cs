using FlyingRat.Module.AttachContent.Models;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Views;
using System;
using System.Threading.Tasks;

namespace FlyingRat.Module.AttachContent.Settings
{
    public class AttachContentPartSettingsDisplayDriver : ContentTypePartDefinitionDisplayDriver
    {
        public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition)
        {
            if (!String.Equals(nameof(AttachContentPartSettings), contentTypePartDefinition.PartDefinition.Name, StringComparison.Ordinal))
            {
                return null;
            }

            return Initialize<AttachContentPartSettingsViewModel>("AttachContentPartSettings_Edit", model =>
            {
                var settings = contentTypePartDefinition.GetSettings<AttachContentPartSettings>();

                model.ShortName = settings.ShortName;
                model.AttachContentPartSettings = settings;

            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
        {
            if (!String.Equals(nameof(AttachContentPartSettings), contentTypePartDefinition.PartDefinition.Name, StringComparison.Ordinal))
            {
                return null;
            }

            var model = new AttachContentPartSettingsViewModel();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.ShortName))
            {
                context.Builder.WithSettings(new AttachContentPartSettings { ShortName = model.ShortName });
            }

            return Edit(contentTypePartDefinition, context.Updater);
        }

    }
}
