namespace TrollStore.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TrollStore.Model;

    public interface ITrollStoreData
    {
        IGenericRepository<Country> Countries { get; }

        IGenericRepository<Customer> Customers { get; }

        IGenericRepository<Manufacturer> Manufacturers { get; }

        IGenericRepository<Product> Products { get; }

        IGenericRepository<ProductType> ProductTypes { get; }

        IGenericRepository<Sale> Sales { get; }

        //IGenericRepository<SaleProduct> SaleProducts { get; }

        IGenericRepository<Store> Stores { get; }

        //IGenericRepository<StoreProduct> StoreProducts { get; }
    }
}
