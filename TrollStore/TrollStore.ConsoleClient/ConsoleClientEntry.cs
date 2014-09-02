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

    public class ConsoleClientEntry
    {
        private const string CountriesXmlFilepath = @"..\..\..\TrollStore.Reports\Raw Data\countries.xml";

        public static void Main()
        {
            var data = new TrollStoreData();

            UploadCountriesToMongoDb();
            InjectMongoDb(data);

            ////string filePathToZip = "..\\..\\..\\TrollStore.Reports\\Raw Data\\SalesReports.zip";
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

        private static void UploadCountriesToMongoDb()
        {
            XmlReporter<CountryFromXml> xmlReporter = new XmlReporter<CountryFromXml>(string.Empty, CountriesXmlFilepath);

            var result = xmlReporter.ReadData();
            Console.WriteLine();
        }

        private static void InjectMongoDb(TrollStoreData data)
        {
            var mongoDbInjector = new MongoDbInjector(data);
            mongoDbInjector.PopulateData();
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
