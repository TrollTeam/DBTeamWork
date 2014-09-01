using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollStore.Reports
{
    public class CustomerPurchaseReport
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int Year { get; set; }
        public decimal PurchasesAmount { get; set; }
    }
}
