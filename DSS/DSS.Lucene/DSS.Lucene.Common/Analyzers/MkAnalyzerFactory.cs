using System.Collections.Generic;
using DSS.Lucene.Common.Utilities;
using Lucene.Net.Analysis;

namespace DSS.Lucene.Common.Analyzers
{
    public class MkAnalyzerFactory:IAnalyzerFactory<Analyzer>
    {
        /// <summary>
        /// Creates an analyzer of the given analyzer type.
        /// </summary>
        /// <returns>T type analyzer.</returns>
        public Analyzer GetAnalyzer()
        {
            var mkStopWords= new SortedSet<string>()
                                          {
                                              
                                          };

            var analyzer = new StopAnalyzer(LVersion.GetActiveVersion(), mkStopWords);
            
            return analyzer;
        }
    }
}
