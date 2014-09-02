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
            this.AddProductTypes();
            this.AddProducts();
        }

        private void AddCountries()
        {
            if (this.trollStoreData.Countries.All().Any())
            {
                return;
            }

            var countries = this.mongoDbCloud.GetItemsFromCollection("countries");

            foreach (var country in countries)
            {
                this.trollStoreData.Countries.Add(new Country()
                {
                    CountryId = int.Parse(country["CountryId"].ToString()),
                    Name = country["Name"].ToString(),
                    CountryCode = country["CountryCode"].ToString(),
                    Currency = country["Currency"].ToString()
                });
            }

            this.SaveChanges();
        }

        private void AddManufacturers()
        {
            if (this.trollStoreData.Manufacturers.All().Any())
            {
                return;
            }

            var manufacturers = this.mongoDbCloud.GetItemsFromCollection("manufacturers");

            foreach (var manufacturer in manufacturers)
            {
                this.trollStoreData.Manufacturers.Add(new Manufacturer()
                {
                    ManufacturerId = int.Parse(manufacturer["ManufacturerId"].ToString()),
                    Name = manufacturer["Name"].ToString(),
                    CountryId = int.Parse(manufacturer["CountryId"].ToString())
                });
            }

            this.SaveChanges();

        }

        private void AddStores()
        {
            if (this.trollStoreData.Stores.All().Any())
            {
                return;
            }

            var stores = this.mongoDbCloud.GetItemsFromCollection("stores");

            foreach (var store in stores)
            {
                this.trollStoreData.Stores.Add(new Store()
                {
                    StoreId = int.Parse(store["StoreId"].ToString()),
                    Name = store["Name"].ToString(),
                    CountryId = int.Parse(store["CountryId"].ToString())
                });
            }

            this.SaveChanges();
        }

        private void AddProductTypes()
        {
            if (this.trollStoreData.ProductTypes.All().Any())
            {
                return;
            }

            var productTypes = this.mongoDbCloud.GetItemsFromCollection("producttypes");

            foreach (var productType in productTypes)
            {
                this.trollStoreData.ProductTypes.Add(new ProductType()
                {
                    ProductTypeId = int.Parse(productType["ProductTypeId"].ToString()),
                    Name = productType["Name"].ToString()
                });
            }

            this.SaveChanges();
        }

        private void AddProducts()
        {
            if (this.trollStoreData.Products.All().Any())
            {
                return;
            }

            var products = this.mongoDbCloud.GetItemsFromCollection("products");

            foreach (var product in products)
            {
                var newProduct = new Product()
                {
                    ProductId = int.Parse(product["ProductId"].ToString()),
                    Name = product["Name"].ToString(),
                    Quantity = int.Parse(product["Quantity"].ToString()),
                    PriceDelivered = int.Parse(product["PriceDelivered"].ToString()),
                    PriceSold = int.Parse(product["PriceSold"].ToString()),
                    ProductTypeId = int.Parse(product["ProductTypeId"].ToString()),
                    ManufacturerId = int.Parse(product["ManufacturerId"].ToString())
                };

                this.trollStoreData.Products.Add(newProduct);
                var storeId = int.Parse(product["StoreId"].ToString());
                var storeForProduct = this.trollStoreData.Stores.All().First(s => s.StoreId == storeId);

                storeForProduct.Products.Add(newProduct);
            }

            this.SaveChanges();
        }

        private void AddCollectionToCloud()
        {

        }

        private void SaveChanges()
        {
            this.trollStoreData.SaveChanges();
        }
    }
}
