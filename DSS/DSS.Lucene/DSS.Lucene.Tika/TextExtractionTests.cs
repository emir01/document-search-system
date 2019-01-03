using System;
using NUnit.Framework;

namespace DSS.Lucene.Tika
{
	[TestFixture]
	public class TextExtractionTests
	{
		private TextExtractor _cut;

		[SetUp]
		public virtual void SetUp()
		{
			_cut = new TextExtractor();
		}


		[Test]
		public void should_extract_contained_filenames_from_zips()
		{
			var textExtractionResult = _cut.Extract("tika.zip");

            Assert.True(textExtractionResult.Text.Contains("Tika.docx"));
            Assert.True(textExtractionResult.Text.Contains("Tika.pptx"));
            Assert.True(textExtractionResult.Text.Contains("tika.xlsx"));
        }

		[Test]
		public void should_extract_from_jpg()
		{
			var textExtractionResult = _cut.Extract("apache.jpg");

			Assert.True(string.IsNullOrWhiteSpace(textExtractionResult.Text));

			Assert.True(textExtractionResult.Metadata["Software"].Contains("Paint.NET"));
            Console.WriteLine(textExtractionResult);
		}

		[Test]
		public void should_extract_from_rtf()
		{
			var textExtractionResult = _cut.Extract("Tika.rtf");
            Assert.True(textExtractionResult.Text.Contains("pack of pickled almonds"));
		}

		[Test]
		public void should_extract_from_pdf()
		{
			var textExtractionResult = _cut.Extract("Tika.pdf");

			Assert.True(textExtractionResult.Text.Contains("pack of pickled almonds"));
		}
		
		[Test]
		public void should_extract_from_docx()
		{
			var textExtractionResult = _cut.Extract("Tika.docx");

			Assert.True(textExtractionResult.Text.Contains("formatted in interesting ways"));
		}

		[Test]
		public void should_extract_from_pptx()
		{
			var textExtractionResult = _cut.Extract("Tika.pptx");

			Assert.True(textExtractionResult.Text.Contains("Tika Test Presentation"));
		}

		[Test]
		public void should_extract_from_xlsx()
		{
			var textExtractionResult = _cut.Extract("Tika.xlsx");

			Assert.True(textExtractionResult.Text.Contains("Use the force duke"));
		}

		[Test]
		public void should_extract_from_doc()
		{
			var textExtractionResult = _cut.Extract("Tika.doc");

			Assert.True(textExtractionResult.Text.Contains("formatted in interesting ways"));
		}

		[Test]
		public void should_extract_from_ppt()
		{
			var textExtractionResult = _cut.Extract("Tika.ppt");

			Assert.True(textExtractionResult.Text.Contains("This document is used for testing"));
		}

		[Test]
		public void should_extract_from_xls()
		{
			var textExtractionResult = _cut.Extract("Tika.xls");

			Assert.True(textExtractionResult.Text.Contains("Use the force duke"));
		}
	}
}