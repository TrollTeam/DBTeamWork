namespace TrollStore.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TrollStore.Data.Contracts;
    using TrollStore.Data.Repositories;
    using TrollStore.Model;

    public class TrollStoreData : ITrollStoreData
    {
        private ITrollStoreContext context;
        private IDictionary<Type, object> repositories;

        public TrollStoreData()
            : this(new TrollStoreContext())
        {
        }

        public TrollStoreData(ITrollStoreContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IGenericRepository<Country> Countries
        {
            get
            {
                return this.GetRepository<Country>();
            }
        }

        public IGenericRepository<Customer> Customers
        {
            get
            {
                return this.GetRepository<Customer>();
            }
        }

        public IGenericRepository<Manufacturer> Manufacturers
        {
            get
            {
                return this.GetRepository<Manufacturer>();
            }
        }

        public IGenericRepository<Product> Products
        {
            get
            {
                return this.GetRepository<Product>();
            }
        }

        public IGenericRepository<ProductType> ProductTypes
        {
            get
            {
                return this.GetRepository<ProductType>();
            }
        }

        public IGenericRepository<Sale> Sales
        {
            get
            {
                return this.GetRepository<Sale>();
            }
        }

        public IGenericRepository<Store> Stores
        {
            get
            {
                return this.GetRepository<Store>();
            }
        }

          public void SaveChanges()
        {
            this.context.SaveChanges();
        }
       
        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);

                //if (typeOfModel.IsAssignableFrom(typeof(Computer)))
                //{
                //    type = typeof(ComputersRepository);
                //}

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}
