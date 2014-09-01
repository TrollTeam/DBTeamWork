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
        public void GenerateReport(IEnumerable<ProductReport> products)
        {
            foreach (var product in products)
            {
                using (var sw = new StreamWriter("..\\..\\" + product.ProductId + ".json"))
                {
                    sw.WriteLine(JsonConvert.SerializeObject(product, Formatting.Indented));
                }
            }
        }
    }
}