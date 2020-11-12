using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
namespace FlyingRat.Module.ContentSetting
{
    public class Migrations: DataMigration
    {
        IContentDefinitionManager _contentDefinitionManager;
        public Migrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }
        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinition("ContentSettingPart", builder => builder
                .Attachable()
                .WithDescription("Provides a Customer Content Setting for your content item."));

            return 1;
        }
    }
}
