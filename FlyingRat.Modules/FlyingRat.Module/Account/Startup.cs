using FlyingRat.Module.Account.Drivers;
using FlyingRat.Module.Account.Events;
using FlyingRat.Module.Account.Handlers;
using FlyingRat.Module.Account.Indexs;
using FlyingRat.Module.Account.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.Data.Migration;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Users;
using OrchardCore.Users.Events;
using OrchardCore.Users.Handlers;
using OrchardCore.Users.Models;
using YesSql.Indexes;

namespace FlyingRat.Module.Account
{
    [Feature("FlyingRat.Module")]
    public class Startup:StartupBase
    {
        public override int Order => 1;
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAccountProfileService,AccountProfileService>();

            services.AddScoped<IDisplayDriver<User>, UserProfileDisplayDriver>();

            services.AddSingleton<IIndexProvider, UserExtensionProvider>();

            services.AddScoped<IDataMigration, Migrations>();
            services.Remove(new ServiceDescriptor(typeof(IContentHandler), typeof(UpdateContentsHandler), ServiceLifetime.Scoped));

            services.AddScoped<IContentHandler, BraksnUpdateContentsHandler>();

            services.AddScoped<ILoginFormEvent, LoginEvent>();
            services.AddScoped<IUserEventHandler, BraksnUserEventHandler>();

            services.Replace(ServiceDescriptor.Scoped<IUserClaimsPrincipalFactory<IUser>, BraksnUserClaimsPrincipalFactory>());
            //services.AddScoped<IRegistrationFormEvents, BraksnRegistrationFormEvents>();
        }
    }
}
