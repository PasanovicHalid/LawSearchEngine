using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using LawSearchEngine.Application.Common.Contracts;
using LawSearchEngine.Application.Common.Services.Interfaces;
using System.Text;

namespace LawSearchEngine.Application.Common.Services.Implementations
{
    public class DocumentReader : IDocumentReader
    {
        public string ReadDocument(FileRequest file)
        {
            PdfReader pdfReader = new(new MemoryStream(file.FileContent));
            PdfDocument pdfDocument = new(pdfReader);

            StringBuilder stringBuilder = new();

            for (int pageNumber = 1; pageNumber <= pdfDocument.GetNumberOfPages(); pageNumber++)
            {
                string pageText = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(pageNumber));
                stringBuilder.Append(pageText);
            }

            string pdfTextContent = stringBuilder.ToString();
            return pdfTextContent;
        }
    }
}
