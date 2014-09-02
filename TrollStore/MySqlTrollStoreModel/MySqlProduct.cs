using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlTrollStoreModel
{
    public class MySqlProduct
    {
        public int ProductID { get; set; }

        public string Name { get; set; }

        public string Manufacturer{ get; set; }

        public string Quantity { get; set; }
        public string Store { get; set; }

    }
}
