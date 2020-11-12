using FlyingRat.Module.Atlas.Drivers;
using FlyingRat.Module.Atlas.Models;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;

namespace FlyingRat.Module.Atlas
{
    [Feature("FlyingRat.Atlas")]
    public class Startup:StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddContentPart<AtlasPart>()
                .UseDisplayDriver<AtlasPartDisplayDriver>();
            services.AddScoped<IDataMigration, Migrations>();
        }
    }
}
