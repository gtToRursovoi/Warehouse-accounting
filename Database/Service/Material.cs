using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Service
{
    public class Material
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CountMaterials { get; set; }
        public string? TypeMaterial { get; set; }
        public virtual ICollection<Product>? Product { get; set; }
    }
}
