using System;
using System.IO;
using System.Text;
using System.Linq;
using TrollStore.Reports.Helpers;
using TrollStore.Data;
using iTextSharp.text;
using System.Collections.Generic;

namespace TrollStore.Reports
{
    public class PdfParser
    {
        public static void GenerateSalesInfoPdf(TrollStoreData data)
        {
            StringBuilder sb = new StringBuilder();

            var sales = data.Sales.All().ToList();
            sb.Append("<div>");
            sb.Append("<h2> Sales Information </h2>");
            sb.Append("<table border='1'>");
            sb.Append("<tr>");
            sb.Append("<th>Sale Id</th>");
            sb.Append("<th>Sale Value</th>");
            sb.Append("<th>Sale Sold Date</th>");
            sb.Append("<th>Customer Name</th>");
            sb.Append("<th>Customer Address</th>");
            sb.Append("</tr>");

            foreach (var sale in sales)
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td>{0}</td>", sale.SaleId);
                sb.AppendFormat("<td>{0}</td>", sale.SaleValue);
                sb.AppendFormat("<td>{0}</td>", sale.Date);
                sb.AppendFormat("<td>{0}</td>", sale.Customer.FullName);
                sb.AppendFormat("<td>{0}</td>", sale.Customer.Country.Name + " -> " + sale.Customer.Address);
                sb.Append("</tr>");
            }

            sb.Append("</table>");
            sb.Append("</div>");

            BuildPdf(sb.ToString());
        }
        public static void GenerateProductInfoPdf(TrollStoreData data)
        {
            StringBuilder sb = new StringBuilder();

            var products = data.Products.All().ToList();

            sb.Append("<div>");
            sb.Append("<h2> Products Information</h2>");
            sb.Append("<table border='1'>");
            sb.Append("<tr>");
            sb.Append("<th>ProductName</th>");
            sb.Append("<th>ProductType</th>");
            sb.Append("<th>Manufacturer</th>");
            sb.Append("<th>PriceDelivered</th>");
            sb.Append("<th>Quantity</th>");
            sb.Append("</tr>");

            decimal price = 0;
            foreach (var product in products)
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td>{0}</td>", product.Name);
                sb.AppendFormat("<td>{0}</td>", product.ProductType.Name);
                sb.AppendFormat("<td>{0}</td>", product.Manufacturer.Name);
                sb.AppendFormat("<td>{0}</td>", product.PriceDelivered);
                sb.AppendFormat("<td>{0}</td>", product.Quantity);
                sb.Append("</tr>");
                price += product.PriceDelivered;
            }

            sb.Append("</table>");
            sb.AppendFormat("<h2>Total sum of deliveries -> {0} </h2>", price);
            sb.Append("</div>");
            
            BuildPdf(sb.ToString());
        }

        private static void BuildPdf(string html)
        {
            PDFBuilder.HtmlToPdfBuilder builder = new PDFBuilder.HtmlToPdfBuilder(PageSize.LETTER);
            //builder.ImportStylesheet("C:\\Users\\Toshiba\\Source\\Repos\\DBTeamWork\\TrollStore\\TrollStore.Reports\\PDF-Data\\Styles\\styles.css"); 
            builder.AddStyle("h2", "text-align: center; color: red; ");

            PDFBuilder.HtmlPdfPage page = builder.AddPage();
            page.AppendHtml(html);
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
