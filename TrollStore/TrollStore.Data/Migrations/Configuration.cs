namespace TrollStore.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TrollStore.Data.TrollStoreContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "TrollStore.Data.TrollStoreContext";
        }

        protected override void Seed(TrollStore.Data.TrollStoreContext context)
        {
        }
    }
}
