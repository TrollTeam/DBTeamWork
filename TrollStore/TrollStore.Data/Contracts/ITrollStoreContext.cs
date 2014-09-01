namespace TrollStore.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TrollStore.Model;

    public interface ITrollStoreContext
    {
        IDbSet<Country> Countries { get; set; }

        IDbSet<Customer> Customers { get; set; }

        IDbSet<Manufacturer> Manufacturers { get; set; }

        IDbSet<Product> Products { get; set; }

        IDbSet<ProductType> ProductTypes { get; set; }

        IDbSet<Sale> Sales { get; set; }

        //IDbSet<SaleProduct> SaleProducts { get; set; }

        IDbSet<Store> Stores { get; set; }

        //IDbSet<StoreProduct> StoreProducts { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void SaveChanges();
    }
}
