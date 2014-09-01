﻿namespace TrollStore.Model
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
        public Store()
        {
            this.Products = new HashSet<Product>();
            this.Sales = new HashSet<Sale>();
        }

        public int StoreId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal SalesPrice { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
