using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollStore.Model
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        public decimal SaleValue { get; set; }

        public DateTime Date { get; set; }

     //   [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
