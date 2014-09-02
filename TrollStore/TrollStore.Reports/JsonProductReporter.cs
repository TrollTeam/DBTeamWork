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
        private const string PATH = "..\\..\\..\\..\\Json-Reports";

        public void GenerateReport(IEnumerable<MySqlProductReport> products)
        {
            foreach (var product in products)
            {
                Directory.CreateDirectory(PATH);

                using (var sw = new StreamWriter(PATH + "\\" + product.ProductID + ".json"))
                {
                    sw.WriteLine(JsonConvert.SerializeObject(product, Formatting.Indented));
                }
            }
        }

        public IEnumerable<MySqlProductReport> ReadJsonData()
        {
            List<MySqlProductReport> reports = new List<MySqlProductReport>();

            if (!Directory.Exists(PATH))
            {
                throw new ArgumentException("The directory Json-Reports does not exist. Try to generate reports first.");
            }

            var allReportPaths = Directory.GetFiles(PATH);

            foreach (var filePath in allReportPaths)
            {
                using (var reader = new StreamReader(filePath))
                {
                    var report = JsonConvert.DeserializeObject<MySqlProductReport>(reader.ReadToEnd());
                    reports.Add(report);
                }
            }

            return reports;
        }
    }
}