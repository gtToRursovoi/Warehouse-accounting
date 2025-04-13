using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Service
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual Material? Material { get; set; }
        public int MaterialId { get; set; }
        public long Articul { get; set; }
        public decimal MinPrice { get; set; }
        public string? ImagePath { get; set; }
        public int PeopleMake { get; set; }
        public string? ManafacturAdrres { get; set; }

        public virtual ICollection<WarehouseProduct>? WarehouseProduct { get; set; }
    }
}
