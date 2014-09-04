namespace TrollStore.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Telerik.OpenAccess;
    using TrollStore.Data;
    using TrollStore.Data.MongoDb;
    using TrollStore.Model;
    using MySqlTrollStoreModel;
    using TrollStore.Reports;
    using SqLite.Data;

    public class ConsoleClientEntry
    {
        private const string CountriesXmlFilepath = @"..\..\..\TrollStore.Reports\RawData\countries.xml";
        private const string FilePathCustomersToZip = @"..\..\..\TrollStore.Reports\RawData\Customers.zip";
        private const string FilePathSalesReportsToZip = @"..\..\..\TrollStore.Reports\RawData\SalesReports.zip";
        private const string ExtractFilePath = @"..\..\..\..\ExcelReports";
        private const string SqliteFilePath = @"..\..\..\..\ExcelFromSQLite\Products.xlsx";

        public static void Main()
        {
            var data = new TrollStoreData();

            //var xmlCountries = ReadCountriesDataFromXml();
            //UploadCountriesDataToCloud(xmlCountries, data);

            //DownloadDataFromCloud(data);

            //ExtractDataFromZip();
            //UploadDataFromExcelToSql(data);

            //PdfParser.GenerateSalesInfoPdf(data);
            //PdfParser.GenerateProductInfoPdf(data);

            //SqliteContext context = new SqliteContext();
            //ExtractExcelFromSQLite extractor = new ExtractExcelFromSQLite(context, SqliteFilePath);
            //extractor.ExctractToExcel(SqliteFilePath);


            //SqliteContext ctx = new SqliteContext();

            //foreach (var item in ctx.Products.ToList())
            //{
            //    Console.WriteLine(item.ProductID + " " + item.SoldPieces + " " + item.StartDate);
            //}


            ExtractDataToMySql(data);
        }


        private static ICollection<CountryFromXml> ReadCountriesDataFromXml()
        {
            XmlReporter<CountryFromXml> xmlReporter = new XmlReporter<CountryFromXml>(string.Empty, CountriesXmlFilepath);
            var xmlCountries = xmlReporter.ReadData().ToList();

            return xmlCountries;
        }

        private static void UploadCountriesDataToCloud(ICollection<CountryFromXml> xmlCountries, TrollStoreData data)
        {
            var mongoDbUploader = new MongoDbCloudConnector(data);

            mongoDbUploader.UploadToCloud(xmlCountries);
        }

        private static void DownloadDataFromCloud(TrollStoreData data)
        {
            var mongoDbDownloader = new MongoDbCloudConnector(data);
            mongoDbDownloader.PopulateData();
        }

        private static void ExtractDataFromZip()
        {
            ExtractZipFile customersExtractor = new ExtractZipFile(FilePathCustomersToZip, ExtractFilePath);
            customersExtractor.ExtractFromZIP();

            ExtractZipFile salesExtractor = new ExtractZipFile(FilePathSalesReportsToZip, ExtractFilePath);
            salesExtractor.ExtractFromZIP();
        }

        private static void UploadDataFromExcelToSql(TrollStoreData data)
        {
            ExcelParser excelCustomersParser = new ExcelParser((ExtractFilePath + "\\Customers\\Customers.xlsx"), data);
            excelCustomersParser.GetCustomersDataFromExcel("Sheet1");

            ExcelParser excelSalesParser = new ExcelParser((ExtractFilePath + "\\09-02-2014\\Sales.xlsx"), data);
            excelSalesParser.GetSalesDataFromExcel("Sheet1");

        }

        private static void ExtractDataToMySql(TrollStoreData sqlDbContext)
        {
            UpdateMySqlDatabase();
            using (var mysqlcontext = new MySqlTrollStoreModel.TrollStoreModel())
            {
                var reporter = new JsonProductReporter();

                var productsFromSql = sqlDbContext.Products.All().Select(p =>
                    new MySqlProductReport
                    {
                        ProductID = p.ProductId,
                        Name = p.Name,
                        Manufacturer = p.Manufacturer.Name,
                        Quantity = p.Quantity,
                    })
                .ToList();

                reporter.GenerateReport(productsFromSql);
                var products = reporter.ReadJsonData();
                foreach (var p in products)
                {
                    mysqlcontext.Add(p);
                }

                mysqlcontext.SaveChanges();
            }
        }
        private static void UpdateMySqlDatabase()
        {
            using (var context = new MySqlTrollStoreModel.TrollStoreModel())
            {
                var schemaHandler = context.GetSchemaHandler();
                EnsureMySqlDatabase(schemaHandler);
            }
        }
        private static void EnsureMySqlDatabase(ISchemaHandler schemaHandler)
        {
            string script = null;
            if (schemaHandler.DatabaseExists())
            {
                script = schemaHandler.CreateUpdateDDLScript(null);
            }
            else
            {
                schemaHandler.CreateDatabase();
                script = schemaHandler.CreateDDLScript();
            }
            if (!string.IsNullOrEmpty(script))
            {
                schemaHandler.ExecuteDDLScript(script);
            }
        }

    }
}
