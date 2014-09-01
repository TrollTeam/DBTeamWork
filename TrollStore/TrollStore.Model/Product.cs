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
        public Product()
        {
            this.Stores = new HashSet<Store>();
        }

        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal PriceDelivered { get; set; }

        //[ForeignKey("ProductType")]
        public int ProductTypeId { get; set; }

        public virtual ProductType ProductType { get; set; }

        //[ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        //[ForeignKey("Store")]
        public virtual ICollection<Store> Stores { get; set; }
    }
}
