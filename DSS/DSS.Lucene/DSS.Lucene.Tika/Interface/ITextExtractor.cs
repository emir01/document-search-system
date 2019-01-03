namespace DSS.Lucene.Tika.Interface
{
    public interface ITextExtractor
    {
        TextExtractionResult Extract(string filePath);
    }
}