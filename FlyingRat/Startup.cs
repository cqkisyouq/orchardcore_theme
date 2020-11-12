using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using OrchardCore.Modules;

namespace FlyingRat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOrchardCms();
            services.Configure<IdentityOptions>(option =>
            {
                Configuration.GetSection("IdentityOptions").Bind(option);
            });
            services.AddHttpsRedirection(option =>
            {
                option.RedirectStatusCode = StatusCodes.Status301MovedPermanently;
            });
            services.Replace(new ServiceDescriptor(typeof(IPoweredByMiddlewareOptions), typeof(BraksnPoweredByMiddlewareOptions), ServiceLifetime.Singleton));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseOrchardCore();
        }
    }
}
