using FlyingRat.Module.ContentSetting.Models;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Views;
using System;
using System.Threading.Tasks;

namespace FlyingRat.Module.ContentSetting.Settings
{
    public class ContentSettingPartSettingsDisplayDriver: ContentTypePartDefinitionDisplayDriver
    {
        public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition)
        {
            if (!String.Equals(nameof(ContentSettingPart), contentTypePartDefinition.PartDefinition.Name, StringComparison.Ordinal))
            {
                return null;
            }

            return Initialize<ContentSettingPartSettingsViewModel>("ContentSettingPartSettings_Edit", model =>
            {
                var settings = contentTypePartDefinition.GetSettings<ContentSettingPartSettings>();
                model.ContentSettingPartSettings = settings;
            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
        {
            if (!String.Equals(nameof(ContentSettingPart), contentTypePartDefinition.PartDefinition.Name, StringComparison.Ordinal))
            {
                return null;
            }
            await Task.Delay(1);
            return Edit(contentTypePartDefinition, context.Updater);
        }

    }
}
