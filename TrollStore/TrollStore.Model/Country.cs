namespace TrollStore.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Country
    {
        private ICollection<Store> stores;

        public Country()
        {
            this.Stores = new HashSet<Store>();
        }

        [Key]
        public int CountryId { get; set; }

        public string Name { get; set; }

        public string CountryCode { get; set; }

        public string Currency { get; set; }

        public virtual ICollection<Store> Stores
        {
            get
            {
                return this.stores;
            }
            set
            {
                this.stores = value;
            }
        }
    }
}
