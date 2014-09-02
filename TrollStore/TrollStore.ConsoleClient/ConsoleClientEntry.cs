namespace TrollStore.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Telerik.OpenAccess;
    using TrollStore.Data;
    using TrollStore.Data.MongoDb;
    using TrollStore.Model;
    using MySqlTrollStoreModel;

    public class ConsoleClientEntry
    {
        public static void Main()
        {
            var data = new TrollStoreData();
            //InjectMongoDb(data);

            data.Countries.Add(new Country
            {
                Name = "Bulgaria",
            });

            data.SaveChanges();

            var newCountry = data.Countries.All().First();
            Console.WriteLine(newCountry.Name);
        }

        private static void InjectMongoDb(TrollStoreData data)
        {
            var mongoDbInjector = new MongoDbInjector(data);
            mongoDbInjector.PopulateData();
        }

        private static void UpdateDatabase()
        {
            using (var context = new MySqlTrollStoreModel.TrollStoreModel())
            {
                var schemaHandler = context.GetSchemaHandler();
                EnsureDB(schemaHandler);
            }
        }
        private static void EnsureDB(ISchemaHandler schemaHandler)
        {
            string script = null;
            if (schemaHandler.DatabaseExists())
            {
                script = schemaHandler.CreateUpdateDDLScript(null);
            }
            else
            {
                schemaHandler.CreateDatabase();
                script = schemaHandler.CreateDDLScript();
            }
            if (!string.IsNullOrEmpty(script))
            {
                schemaHandler.ExecuteDDLScript(script);
            }
        }

    }
}
