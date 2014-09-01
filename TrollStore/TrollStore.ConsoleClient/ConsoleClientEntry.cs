namespace TrollStore.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TrollStore.Data;
    using TrollStore.Model;

    public class ConsoleClientEntry
    {
        public static void Main()
        {
            var data = new TrollStoreData();

            data.Countries.Add(new Country
            {
                Name = "Bulgaria",
            });

            data.SaveChanges();

            var newCountry = data.Countries.All().First();
            Console.WriteLine(newCountry.Name);
        }
    }
}
