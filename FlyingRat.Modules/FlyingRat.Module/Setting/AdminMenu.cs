using FlyingRat.Module.Setting.Drivers;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlyingRat.Module.Setting
{
    public class AdminMenu : INavigationProvider
    {
        IStringLocalizer S;
        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            S = localizer;
        }
        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder.Add(S["Configuration"], configuration => configuration
               .Add(S["Settings"], "1", settings => settings
                   .Add(S["内容发布"], "1", entry => entry
                   .AddClass("publishSetting").Id("publishSetting")
                       .Action("Index", "Admin", new { area = "FlyingRat.Module", groupId = PublishSettingDisplayDriver.groupId })
                       //.Permission(Permissions.ManageGroupSettings)
                       .LocalNav()
                   )
               )
           );
            return Task.CompletedTask;
        }
    }
}
