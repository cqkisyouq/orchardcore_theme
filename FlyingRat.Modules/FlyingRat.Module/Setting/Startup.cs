using FlyingRat.Module.Setting.Drivers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Settings;
using System;

namespace FlyingRat.Module.Setting
{
    [Feature("FlyingRat.PublishSetting")]
    public class Startup: StartupBase
    {
        private readonly AdminOptions _adminOptions;
        public Startup(IOptions<AdminOptions> adminOptions)
        {
            _adminOptions = adminOptions.Value;
        }
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDisplayDriver<ISite>,PublishSettingDisplayDriver>();
            services.AddScoped<INavigationProvider, AdminMenu>();
        }
        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            // Admin
            routes.MapAreaControllerRoute(
                name: "AdminPublishSettings",
                areaName: "FlyingRat.Module",
                pattern: _adminOptions.AdminUrlPrefix + "/PublishSettings/{groupId}",
                defaults: new { controller = "Admin", action = "Index" }
            );
        }
    }
}
