using Lucene.Net.Search;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlyingRat.Module.Services
{
    public interface IBraksnSearchQueryService
    {
        Task<Tuple<List<string>,int>> ExecuteQueryAsync(Query query, string indexName, int start = 0, int end = 20);
    }
}
