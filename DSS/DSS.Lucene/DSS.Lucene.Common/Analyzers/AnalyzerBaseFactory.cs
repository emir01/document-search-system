using DSS.Lucene.Common.Utilities;
using Lucene.Net.Analysis.Standard;

namespace DSS.Lucene.Common.Analyzers
{
    /// <summary>
    /// The basic implementation of an AnalyzerFactory that creates basic Lucene analyzers
    /// </summary>
    public class AnalyzerBaseFactory:IAnalyzerFactory<StandardAnalyzer>
    {
        /// <summary>
        /// Creates an analyzer of the given analyzer type.
        /// </summary>
        /// <returns>T type analyzer.</returns>
        public StandardAnalyzer GetAnalyzer()
        {
            var analyzer = new StandardAnalyzer(LVersion.GetActiveVersion());
            return analyzer;
        }
    }
}
