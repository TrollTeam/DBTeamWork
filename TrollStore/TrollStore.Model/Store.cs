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
        private ICollection<Product> products;

        private ICollection<Sale> sales;

        public Store()
        {
            this.products = new HashSet<Product>();
            this.sales = new HashSet<Sale>();
        }

        public int StoreId { get; set; }

        public string Name { get; set; }      

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Product> Products
        {
            get
            {
                return this.products;
            }
            set
            {
                this.products = value;
            }
        }

        public virtual ICollection<Sale> Sales
        {
            get
            {
                return this.sales;
            }
            set
            {
                this.sales = value;
            }
        }
    }
}
