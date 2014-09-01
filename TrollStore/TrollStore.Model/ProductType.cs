namespace TrollStore.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProductType
    {
        [Key]
        public int ProductTypeId { get; set; }

        public string Name { get; set; }
    }
}
