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
        private const string FilePathCustomersToZip = "..\\..\\..\\TrollStore.Reports\\RawData\\Customers.zip";
        private const string FilePathSalesReportsToZip = "..\\..\\..\\TrollStore.Reports\\RawData\\SalesReports.zip";
        private const string ExtractFilePath = "..\\..\\..\\..\\ExcelReports";

        public static void Main()
        {
            var data = new TrollStoreData();

            //var xmlCountries = ReadCountriesDataFromXml();
            //UploadCountriesDataToCloud(xmlCountries, data);

            //DownloadDataFromCloud(data);

            //ExtractDataFromZip();
            //UploadDataFromExcelToSql(data);

            SqliteContext context = new SqliteContext();
            ExtractExcelFromSQLite extractor = new ExtractExcelFromSQLite(context);
            extractor.ExctractToExcel(@"C:\Users\aniad_000\Documents\GitHub\DBTeamWork\ExcelFromSQLite\Products.xlsx");
            

            //SqliteContext ctx = new SqliteContext();

            //var entry = new SqliteProduct()
            //{
            //    ProductID = 131,
            //    SoldPieces = 205,
            //    StartDate = DateTime.Now,
            //    EndDate = DateTime.Now

            //};

            //ctx.Products.Add(entry);
            //var res = ctx.SaveChanges();
            //Console.WriteLine("PESHO " + res);

            //foreach (var item in ctx.Products.ToList())
            //{
            //    Console.WriteLine(item.ProductID + " " + item.SoldPieces + " "+ item.StartDate);
            //}



            //UploadCountriesDataToCloud(data);
            //DownloadDataFromCloud(data);

            ////string filePathToZip = "..\\..\\..\\TrollStore.Reports\\RawData\\SalesReports.zip";
            //string extractFilePath = "..\\..\\..\\..\\ExcelReports";
            ////ExtractZipFile extractor = new ExtractZipFile(filePathToZip, extractFilePath);
            ////extractor.ExtractFromZIP();

            //ExcelSalesParser excelParser = new ExcelSalesParser((extractFilePath + "\\09-02-2014\\Sales.xlsx"), data);
            //excelParser.GetDataFromExcel("Sheet1");


            //UpdateDatabase();
            //using (var mysqlcontext = new MySqlTrollStoreModel.TrollStoreModel())
            //{
            //    var reporter = new JsonProductReporter();
            //    var fakedata = new List<MySqlProductReport>();
            //    fakedata.Add(new MySqlProductReport() { Name = "ivan", Manufacturer = "bai ivan" });
            //    reporter.GenerateReport(fakedata);
            //    var products = reporter.ReadJsonData();
            //    foreach (var p in products)
            //    {
            //        mysqlcontext.Add(p);
            //    }

            //    mysqlcontext.SaveChanges();
            //}



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

        private static void UpdateDatabase()
        {
            using (var context = new MySqlTrollStoreModel.TrollStoreModel())
            {
                var schemaHandler = context.GetSchemaHandler();
                EnsureDB(schemaHandler);
            }
        }
        private static void EnsureDB(ISchemaHandler schemaHandler)
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
