using FlyingRat.Module.ContentSetting.Models;
using FlyingRat.Module.ContentSetting.Settings;
using FlyingRat.Module.ContentSetting.ViewModel;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System.Linq;
using System.Threading.Tasks;

namespace FlyingRat.Module.ContentSetting.Drivers
{
    public class ContentSettingPartDisplayDriver : ContentPartDisplayDriver<ContentSettingPart>
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        public ContentSettingPartDisplayDriver(IContentDefinitionManager contentDefinitionManager
            )
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public override IDisplayResult Display(ContentSettingPart part)
        {
            return null;
        }

        public override IDisplayResult Edit(ContentSettingPart part)
        {
            return Initialize<ContentSettingPartViewModel>("ContentSettingPart_Edit", m => BuildViewModel(m, part));
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentSettingPart model, IUpdateModel updater)
        {
            await updater.TryUpdateModelAsync(model, Prefix,x=>x.HasCoverList);
            return Edit(model);
        }
        public ContentSettingPartSettings GetAttachContentPartSettings(ContentSettingPart part)
        {
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(part.ContentItem.ContentType);
            var contentTypePartDefinition = contentTypeDefinition.Parts.FirstOrDefault(p => p.PartDefinition.Name == nameof(ContentSettingPart));
            var settings = contentTypePartDefinition.GetSettings<ContentSettingPartSettings>();
            return settings;
        }
        private void BuildViewModel(ContentSettingPartViewModel model, ContentSettingPart part)
        {
            var settings = GetAttachContentPartSettings(part);

            model.ContentItem = part.ContentItem;
            model.ContentSettingPart = part;
            model.HasCoverList = part.HasCoverList;
            model.Settings = settings;
        }
    }
}
