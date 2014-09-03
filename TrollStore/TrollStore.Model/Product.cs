namespace TrollStore.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal PriceDelivered { get; set; }

        public decimal PriceSold { get; set; }

        public int Quantity { get; set; }

        public int ProductTypeId { get; set; }

        public virtual ProductType ProductType { get; set; }

        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}