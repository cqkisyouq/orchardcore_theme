using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
namespace FlyingRat.Module.AttachContent
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
            _contentDefinitionManager.AlterPartDefinition("AttachContentPart", builder => builder
                .Attachable()
                .WithDescription("Provides a Customer comment section for your content item."));

            return 1;
        }
    }
}
