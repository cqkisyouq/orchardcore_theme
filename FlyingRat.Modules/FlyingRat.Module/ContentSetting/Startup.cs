using FlyingRat.Module.ContentSetting.Drivers;
using FlyingRat.Module.ContentSetting.Models;
using FlyingRat.Module.ContentSetting.Settings;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;

namespace FlyingRat.Module.ContentSetting
{
    [Feature("FlyingRat.ContentSetting")]
    public class Startup:StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddContentPart<ContentSettingPart>()
                  .UseDisplayDriver<ContentSettingPartDisplayDriver>();
                  
            services.AddScoped<IContentTypePartDefinitionDisplayDriver, ContentSettingPartSettingsDisplayDriver>();
            services.AddScoped<IDataMigration, Migrations>();
        }
    }
}
