using Lucene.Net.Analysis;
using System;

namespace FlyingRat.Module.LuncenExtensions
{
    public class JiabaLunceAnalyzer : OrchardCore.Lucene.ILuceneAnalyzer
    {
        private readonly Func<Analyzer> _factory;
        public JiabaLunceAnalyzer(string name, Analyzer analyzer):this(name,()=>analyzer)
        {
        }

        public JiabaLunceAnalyzer(string name, Func<Analyzer> factory)
        {
            Name = name;
            _factory = factory;
        }

        public string Name { get; }

        public Analyzer CreateAnalyzer()
        {
            return _factory();
        }
    }
}
