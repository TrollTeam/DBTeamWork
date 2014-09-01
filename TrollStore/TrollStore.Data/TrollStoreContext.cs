namespace TrollStore.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TrollStore.Data.Contracts;
    using TrollStore.Data.Migrations;

    using TrollStore.Model;

    public class TrollStoreContext : DbContext, ITrollStoreContext
    {
        public TrollStoreContext()
            : base("TrollStoreConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TrollStoreContext, Configuration>());
        }

        public IDbSet<Country> Countries { get; set; }

        public IDbSet<Customer> Customers { get; set; }

        public IDbSet<Manufacturer> Manufacturers { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<ProductType> ProductTypes { get; set; }

        public IDbSet<Sale> Sales { get; set; }

       /// public IDbSet<SaleProduct> SaleProducts { get; set; }

        public IDbSet<Store> Stores { get; set; }

       // public IDbSet<StoreProduct> StoreProducts { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
