using FlyingRat.Module.AttachContent.Drivers;
using FlyingRat.Module.AttachContent.Handlers;
using FlyingRat.Module.AttachContent.Models;
using FlyingRat.Module.AttachContent.Services;
using FlyingRat.Module.AttachContent.Settings;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.DisplayManagement.Descriptors;
using OrchardCore.Modules;

namespace FlyingRat.Module.AttachContent
{
    [Feature("FlyingRat.AttachContent")]
    public class Startup:StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // Liquid Part
            services.AddScoped<IShapeTableProvider, AttachContentShape>();
            services.AddContentPart<AttachContentPart>()
                  .UseDisplayDriver<AttachContentPartDisplayDriver>()
                  .AddHandler<AttchContentPartHandler>();
                  
            services.AddScoped<IContentTypePartDefinitionDisplayDriver, AttachContentPartSettingsDisplayDriver>();
            services.AddScoped<IDataMigration, Migrations>();
        }
    }
}
