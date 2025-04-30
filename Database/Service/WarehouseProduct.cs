using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Service
{
    public class WarehouseProduct
    {
        public int Id { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
        public int WarehouseId { get; set; }
        public virtual Product? Product { get; set; }
        public int ProductId { get; set; }
        public long CountProduct { get; set; }

        public virtual ICollection<Sales>? Sales { get; set; }
        public override string ToString()
        {
            return Product != null ? Product.Name : "Без названия";
        }

    }
}
