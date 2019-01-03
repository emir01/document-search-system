using System;
using System.Collections.Generic;
using System.Linq;
using DSS.Lucene.Tika.Interface;
using java.io;
using java.lang;
using javax.xml.transform;
using javax.xml.transform.sax;
using javax.xml.transform.stream;
using org.apache.tika.io;
using org.apache.tika.metadata;
using org.apache.tika.parser;
using Exception = System.Exception;
using String = System.String;
using StringBuilder = System.Text.StringBuilder;

namespace DSS.Lucene.Tika
{
	public class TextExtractionResult
	{
		public string Text { get; set; }
		public string ContentType { get; set; }
		public IDictionary<string, string> Metadata { get; set; }

		public override string ToString()
		{
			var builder = new StringBuilder("Text:\n" + Text + "MetaData:\n");

			foreach (var keypair in Metadata)
			{
				builder.AppendFormat("{0} - {1}\n", keypair.Key, keypair.Value);
			}

			return builder.ToString();
		}
	}

	public class TextExtractor:ITextExtractor
	{
		private StringWriter _outputWriter;

		public TextExtractionResult Extract(string filePath)
		{
			var parser = new AutoDetectParser();
			var metadata = new Metadata();
			var parseContext = new ParseContext();
			Class parserClass = parser.GetType();
			parseContext.set(parserClass, parser);

			try
			{
				var file = new File(filePath);
				var url = file.toURI().toURL();
				using (InputStream inputStream = TikaInputStream.get(url, metadata))
				{
					parser.parse(inputStream, getTransformerHandler(), metadata, parseContext);
					inputStream.close();
				}

				return assembleExtractionResult(_outputWriter.toString(), metadata);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Extraction of text from the file '{0}' failed.".ToFormat(filePath), ex);
			}
		}

		private static TextExtractionResult assembleExtractionResult(string text, Metadata metadata)
		{
			Dictionary<string, string> metaDataResult = metadata.names().ToDictionary(name => name,
			                                                                          name =>
			                                                                          String.Join(", ", metadata.getValues(name)));

			string contentType = metaDataResult["Content-Type"];

			return new TextExtractionResult
			       {
			       	Text = text,
			       	ContentType = contentType,
			       	Metadata = metaDataResult
			       };
		}

		private TransformerHandler getTransformerHandler()
		{
			var factory = TransformerFactory.newInstance() as SAXTransformerFactory;
			TransformerHandler handler = factory.newTransformerHandler();
			handler.getTransformer().setOutputProperty(OutputKeys.METHOD, "text");
			handler.getTransformer().setOutputProperty(OutputKeys.INDENT, "yes");

			_outputWriter = new StringWriter();
			handler.setResult(new StreamResult(_outputWriter));
			return handler;
		}
	}
}