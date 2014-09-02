namespace SQLiteDatabase.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Product
    {

        public int ProductId { get; set; }

        public int ExternalProductId { get; set; }

        public int SoldPieces { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndTime { get; set; }
    }
}
