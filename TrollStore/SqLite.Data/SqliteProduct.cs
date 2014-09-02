using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqLite.Data
{
    public class SqliteProduct
    {
        [Key]
        public int ProductID { get; set; }

        public int SoldPieces { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }


    }
}
