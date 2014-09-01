namespace TrollStore.Data.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TrollStore.Model;

    public class MongoDbInjector
    {
        private readonly MongoDbCloud mongoDbCloud;
        private readonly TrollStoreData trollStoreData;

        public MongoDbInjector(TrollStoreData mongoDbData)
            : this(mongoDbData, new MongoDbCloud())
        {
        }

        public MongoDbInjector(TrollStoreData mongoDbData, MongoDbCloud mongoDb)
        {
            this.trollStoreData = mongoDbData;
            this.mongoDbCloud = mongoDb;
        }

        public void PopulateData()
        {
            this.AddCountries();
            this.AddManufacturers();
            this.AddStores();
        }

        private void AddCountries()
        {
            //if (this.trollStoreData.Countries.All().Any())
            //{
            //    return;
            //}

            var countries = this.mongoDbCloud.GetItemsFromCollection("countries");

            foreach (var country in countries)
            {
                this.trollStoreData.Countries.Add(new Country()
                {
                    CountryId = int.Parse(country["id"].ToString()),
                    Name = country["name"].ToString()
                });
            }

            this.SaveChanges();
        }

        private void AddManufacturers()
        {
            //if (this.trollStoreData.Manufacturers.All().Any())
            //{
            //    return;
            //}

            var manufacturers = this.mongoDbCloud.GetItemsFromCollection("manufacturers");

            foreach (var manufacturer in manufacturers)
            {
                this.trollStoreData.Manufacturers.Add(new Manufacturer()
                {
                    ManufacturerId = int.Parse(manufacturer["id"].ToString()),
                    Name = manufacturer["name"].ToString(),
                    CountryId = int.Parse(manufacturer["countryId"].ToString())
                });
            }

            this.SaveChanges();
        }

        private void AddStores()
        {
            //if (this.trollStoreData.Manufacturers.All().Any())
            //{
            //    return;
            //}

            var stores = this.mongoDbCloud.GetItemsFromCollection("stores");

            foreach (var store in stores)
            {
                this.trollStoreData.Stores.Add(new Store()
                {
                    StoreId = int.Parse(store["id"].ToString()),
                    Name = store["name"].ToString(),
                    CountryId = int.Parse(store["countryId"].ToString())
                });
            }

            this.SaveChanges();
        }

        private void SaveChanges()
        {
            this.trollStoreData.SaveChanges();
        }
    }
}
