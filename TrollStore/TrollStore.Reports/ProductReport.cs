using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollStore.Reports
{
    public class ProductReport
    {
        public int ProductId { get; set; }

        public int TypeId { get; set; }

        public int ManufacturerId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ProductReport()
        {
        }

        public ProductReport(int productId, int typeId, int manufacturerId, string name, decimal price)
        {
            this.ProductId = productId;
            this.TypeId = typeId;
            this.ManufacturerId = manufacturerId;
            this.Name = name;
            this.Price = price;
        }
    }
}