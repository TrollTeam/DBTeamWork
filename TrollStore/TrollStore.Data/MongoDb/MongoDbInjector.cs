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

        public void AddCountries()
        {
            //if (this.mongoDbData.Countries.All().Any())
            //{
            //    return;
            //}

            var countries = this.mongoDbCloud.GetItemsFromCollection("countries");

            foreach (var country in countries)
            {
                this.trollStoreData.Countries.Add(new Country()
                {
                    Name = country["name"].ToString()
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
