using OrchardCore;
using OrchardCore.Lucene;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Settings;
using System.Threading.Tasks;
using OrchardCore.Lucene.Model;
using OrchardCore.Entities;
using OrchardCore.Lucene.Services;

namespace FlyingRat.Module.Helper
{
    public static class LucenceHelper
    {
        //public static async Task<List<string>> Tokenize(this IOrchardHelper orchardHelper, string filed,params string[] text)
        //{
        //    var siteSettings = await orchardHelper.HttpContext.RequestServices.GetService<ISiteService>()?.GetSiteSettingsAsync();
        //    var searchSettings = siteSettings.As<LuceneSettings>();
        //    var luceneIndexSettingsService = orchardHelper.HttpContext.RequestServices.GetService<LuceneIndexSettingsService>();
        //    var luceneAnalyzerManager = orchardHelper.HttpContext.RequestServices.GetService<LuceneAnalyzerManager>();
        //    var luceneIndexSettings = await luceneIndexSettingsService.GetSettingsAsync(searchSettings.SearchIndex);
        //    var analyzer = luceneAnalyzerManager.CreateAnalyzer(await luceneIndexSettingsService.GetIndexAnalyzerAsync(luceneIndexSettings.IndexName));
        //    var result = LuceneQueryService.Tokenize(filed, string.Join(",", text), analyzer).Distinct().ToList();
        //    return result;
        //}
    }
}
