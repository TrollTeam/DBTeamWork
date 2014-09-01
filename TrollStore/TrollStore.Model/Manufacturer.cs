namespace TrollStore.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Manufacturer
    {
        public Manufacturer()
        {
            this.Products = new HashSet<Product>();
        }

        [Key]
        public int ManufacturerId { get; set; }

        public string Name { get; set; }

        //[ForeignKey("Country")]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        //[ForeignKey("Product")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
