using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Service
{
    public class Agent
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public long INN { get; set; }
        public string? LegalAdress { get; set; }
        public string? Directro { get; set; }

        public virtual ICollection<Sales>? Sales { get; set; }
    }
}
