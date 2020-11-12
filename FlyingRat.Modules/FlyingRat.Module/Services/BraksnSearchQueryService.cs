﻿using Lucene.Net.Search;
using OrchardCore.Lucene;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlyingRat.Module.Services
{
    public class BraksnSearchQueryService : IBraksnSearchQueryService
    {
        private readonly LuceneIndexManager _luceneIndexManager;

        private static HashSet<string> IdSet = new HashSet<string>(new string[] { "ContentItemId" });

        public BraksnSearchQueryService(LuceneIndexManager luceneIndexManager)
        {
            _luceneIndexManager = luceneIndexManager;
        }
        public async Task<Tuple<List<string>, int>> ExecuteQueryAsync(Query query, string indexName, int start = 0, int end = 20)
        {
            var contentItemIds = new List<string>();
            var totalCount = 0;
            await _luceneIndexManager.SearchAsync(indexName, searcher =>
            {
                var collector = TopScoreDocCollector.Create(end, true);

                searcher.Search(query, collector);
                var hits = collector.GetTopDocs(start, end);
                totalCount = hits.TotalHits;
                foreach (var hit in hits.ScoreDocs)
                {
                    var d = searcher.Doc(hit.Doc, IdSet);
                    contentItemIds.Add(d.GetField("ContentItemId").GetStringValue());
                }

                return Task.CompletedTask;
            });

            return Tuple.Create(contentItemIds, totalCount);
        }

    }
}
