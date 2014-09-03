using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SqLite.Data.Migrations;


namespace SqLite.Data
{
    public class SqliteContext : DbContext
    {
        public SqliteContext()
            : base("SQLITE_URI")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SqliteContext, Configuration>());
        }

        public IDbSet<SqliteProduct> Products { get; set; }


    }
}
