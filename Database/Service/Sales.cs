using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Service
{
    public class Sales
    {
        public int Id { get; set; }
        public virtual Agent? Agent { get; set; }
        public int AgentId { get; set; }
        public virtual WarehouseProduct? WarehouseProduct { get; set; }
        public int WarehouseProductId { get; set; }
        public int SalesCount { get; set; }
        public string SalesDate { get; set; }

    }
}
