namespace TrollStore.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        //[ForeignKey("Country")]
        public int CountrtyId { get; set; }

        public virtual Country Country { get; set; }
    }
}
