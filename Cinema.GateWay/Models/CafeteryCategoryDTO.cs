using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.GateWay.Models
{
    public class CafeteryCategoryDTO
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public IEnumerable<ProductDTO> Products { get; set; }
    }
}
