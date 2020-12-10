using OrchardCore;
using OrchardCore.Lucene;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Lucene.Services;
using OrchardCore.Settings;
using OrchardCore.Lucene.Model;
using OrchardCore.Entities;
using System.Collections.Generic;
using System.Linq;
public static class ContentRazorHelperExtensions
{
    public static async ValueTask<List<string>> Tokenize(this IOrchardHelper orchardHelper,string field,params string[] text)
    {
        var analyzerManager = orchardHelper.HttpContext.RequestServices.GetService<LuceneAnalyzerManager>();
        var luceneIndexSettingsService = orchardHelper.HttpContext.RequestServices.GetService<LuceneIndexSettingsService>();
        var siteSettings = await orchardHelper.HttpContext.RequestServices.GetService<ISiteService>()?.GetSiteSettingsAsync();
        var searchSettings = siteSettings.As<LuceneSettings>();
        var luceneIndexSettings = await luceneIndexSettingsService.GetSettingsAsync(searchSettings.SearchIndex);
        var analyzerName = await luceneIndexSettingsService.LoadIndexAnalyzerAsync(luceneIndexSettings.IndexName);
        var analyzer = analyzerManager.CreateAnalyzer(analyzerName);
        var tokens = LuceneQueryService.Tokenize(field, string.Join(" ", text), analyzer).Distinct();
        return tokens.ToList();
    }
}