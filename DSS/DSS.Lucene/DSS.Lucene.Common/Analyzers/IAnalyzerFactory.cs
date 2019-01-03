using Lucene.Net.Analysis;

namespace DSS.Lucene.Common.Analyzers
{
    /// <summary>
    /// Basic interface that describes code functionality for creating Lucene Analyzers.
    /// </summary>
    /// <typeparam name="T">The type of analazyer the factory is responsible to create.</typeparam>
    public interface IAnalyzerFactory<out T> where T:Analyzer
    {
        /// <summary>
        /// Creates an analyzer of the given analyzer type.
        /// </summary>
        /// <returns>T type analyzer.</returns>
        T GetAnalyzer();
    }
}
