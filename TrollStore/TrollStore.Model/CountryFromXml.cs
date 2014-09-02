namespace TrollStore.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CountryFromXml
    {
        public CountryFromXml()
        {
            
        }

        [Key]
        public int CountryId { get; set; }

        public string Name { get; set; }

        public string CountryCode { get; set; }

        public string Currency { get; set; }
    }
}
