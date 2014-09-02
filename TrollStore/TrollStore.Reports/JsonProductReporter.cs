using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollStore.Reports
{
    public class JsonProductReporter
    {
        public void GenerateReport(IEnumerable<MySqlProductReport> products)
        {
            foreach (var product in products)
            {
                using (var sw = new StreamWriter("..\\..\\Json-Reports" + product.ProductID + ".json"))
                {
                    sw.WriteLine(JsonConvert.SerializeObject(product, Formatting.Indented));
                }
            }
        }

        public IEnumerable<MySqlProductReport> ReadJsonData()
        {
            List<MySqlProductReport> reports = new List<MySqlProductReport>();
            string jsonReportsDirectoryPath = "..\\..\\Json-Reports";

            var allReportPaths = Directory.GetFiles(jsonReportsDirectoryPath);

            foreach (var filePath in allReportPaths)
            {
                using (var sr = new StreamReader(filePath))
                {
                    var report = JsonConvert.DeserializeObject<MySqlProductReport>(sr.ReadToEnd());
                    reports.Add(report);
                }
            }

            return reports;
        }
    }
}