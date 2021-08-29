using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Cafetery.Models
{
    public class CategoryDTO
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public IEnumerable<ProductDTO> Products { get; set; }
    }
}
