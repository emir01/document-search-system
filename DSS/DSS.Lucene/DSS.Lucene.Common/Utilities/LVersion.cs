using Lucene.Net.Util;

namespace DSS.Lucene.Common.Utilities
{
    public static class LVersion
    {
        public static Version GetActiveVersion()
        {
            return Version.LUCENE_30;
        }
    }
}

