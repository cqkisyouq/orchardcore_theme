using FlyingRat.Module.Atlas.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using System.Net.NetworkInformation;

namespace FlyingRat.Module.Atlas
{
    public  class Migrations: DataMigration
    {
        private IContentDefinitionManager _contentDefinitionManager;
        public Migrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }
        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinition(nameof(AtlasPart), builder =>
            {
                builder.Attachable()
                    .WithDescription("Provides a atals for you item");
            });
            return 1;
        }
    }
}
