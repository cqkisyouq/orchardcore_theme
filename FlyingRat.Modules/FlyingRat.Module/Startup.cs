using FlyingRat.Module.Controllers;
using FlyingRat.Module.Drivers;
using FlyingRat.Module.LuncenExtensions;
using FlyingRat.Module.Services;
using jieba.NET;
using JiebaNet.Segmenter;
using Markdig;
using Markdig.Parsers.Inlines;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Lists.Drivers;
using OrchardCore.Lists.Models;
using OrchardCore.Media.Events;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Taxonomies.Drivers;
using System;
namespace FlyingRat.Module
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {

            var jieBaAnalyzer = new JieBaAnalyzer(TokenizerMode.Search);

            services.Configure<OrchardCore.Lucene.LuceneOptions>(option =>
            {
                option.Analyzers.Add(new JiabaLunceAnalyzer("jiebaAnalyzer", jieBaAnalyzer));
            });

            services.AddScoped<IContentDisplayDriver, BraksnTermPartContentDriver>();

            services.AddScoped<IBraksnSearchQueryService, BraksnSearchQueryService>();

            //services.AddSingleton<IMediaCreatingEventHandler, BraksnMediaCreateEvent>();
        }
        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            var contentController = typeof(ContentController).ControllerName();
            routes.MapAreaControllerRoute(
                name: "Lucene.BraksnSearch",
                areaName: "FlyingRat.Module",
                pattern: "SearchForm",
                defaults: new { controller = "Search", action = "Search" }
            );

            routes.MapAreaControllerRoute(
               name: "Content.Select",
               areaName: "FlyingRat.Module",
               pattern: "Content/Index",
               defaults: new { controller = contentController, action = nameof(ContentController.Index) }
           );

            routes.MapAreaControllerRoute(
                name: "Content.Create",
                areaName: "FlyingRat.Module",
                pattern: "Content/Create",
                defaults: new { controller = contentController, action =nameof(ContentController.Create) }
            );
        }
    }

    [Feature("FlyingRat.Module")]
    public class StartupMarkdown : StartupBase
    {
        public override int Order => 1;
        public override void ConfigureServices(IServiceCollection services)
        {
            services.Remove(new ServiceDescriptor(typeof(IContentDisplayDriver), typeof(TermPartContentDriver), ServiceLifetime.Scoped));
            
            services.AddContentPart<ListPart>()
                .RemoveDisplayDriver<ListPartDisplayDriver>()
                .UseDisplayDriver<BraksnListPartDriver>();

            //services.ConfigureMarkdownPipeline(pipe =>
            //{
            //    //var parser = pipe.BlockParsers.Find<HtmlBlockParser>();
            //    //if (parser == null)
            //    //{
            //    //    pipe.BlockParsers.Add(new HtmlBlockParser());
            //    //}
            //    pipe.InlineParsers.TryFind(out AutolineInlineParser inlineParser);
            //    if (inlineParser != null)
            //    {
            //        inlineParser.EnableHtmlParsing = true;
            //    }
            //    //pipe.UseAdvancedExtensions();
            //});

        }
    }
}
