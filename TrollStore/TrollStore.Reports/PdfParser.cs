using System;
using System.IO;
using System.Text;
using System.Linq;
using TrollStore.Reports.Helpers;
using TrollStore.Reports.Helpers;
using TrollStore.Data;
using iTextSharp.text;

namespace TrollStore.Reports
{
    public class PdfParser
    {
        public static void GeneratePdf()
        {
            StringBuilder sb = new StringBuilder();
            var data = new TrollStoreData();

            sb.Append("<table border='1'>");
            sb.Append("<tr>");
            sb.Append("<th>CountryName</th>");
            sb.Append("<th>Currency</th>");
            sb.Append("<th>CountryCode</th>");
            sb.Append("</tr>");

            data.Countries.All().ToList().ForEach(b =>
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td>{0}</td>", b.Name);
                sb.AppendFormat("<td>{0}</td>", b.Currency);
                sb.AppendFormat("<td>{0}</td>", b.CountryCode);
                sb.Append("</tr>");
            });
            sb.Append("</table>");

            PDFBuilder.HtmlToPdfBuilder builder = new PDFBuilder.HtmlToPdfBuilder(PageSize.LETTER);
            //builder.ImportStylesheet(AppDomain.CurrentDomain.BaseDirectory + "style.css"); 

            PDFBuilder.HtmlPdfPage page = builder.AddPage();
            page.AppendHtml(sb.ToString());
            byte[] file = builder.RenderPdf();

            string tempFolder = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..\\PdfResult\\";
            string tempFileName = DateTime.Now.ToString("yyyy-MM-dd") + "-" + Guid.NewGuid() + ".pdf";
            if (Helpers.Helpers.DirectoryExist(tempFolder))
            {
                if (!File.Exists(tempFolder + tempFileName))
                    File.WriteAllBytes(tempFolder + tempFileName, file);
            }
        }
    }
} 
