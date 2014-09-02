using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteDatabase.Models;

namespace SQLiteDatabase.Data
{
    public class SQLiteProductContext : DbContext
    {
        public SQLiteProductContext()
            : base(@"Data Source=..\\..\\ProductsDatabase\SQLiteDatabase.sqlite; Version=3;")
        {
            Database.SetInitializer<SQLiteProductContext>(null);
        }

        public DbSet<Product> Products { get; set; }
    }
}
