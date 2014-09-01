namespace TrollStore.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Store
    {
        // ICollection<Product> products;

        public Store()
        {
            //this.products = new HashSet<Product>();
            //this.Products = new HashSet<Product>();
        }

        [Key]
        public int StoreId { get; set; }

        public int Quantity { get; set; }

        public decimal SalesPrice { get; set; }

        //[ForeignKey("Product")]
        //public virtual ICollection<Product> Products
        //{
        //    get;
        //    //{
        //    //    //return this.products;
        //    //}
        //    set;
        //    //{
        //    //    //this.products = value;
        //    //}
        //}

        //[ForeignKey("Country")]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
