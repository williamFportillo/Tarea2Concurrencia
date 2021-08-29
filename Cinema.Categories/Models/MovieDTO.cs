using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Movies.Models
{
    public class MovieDTO
    {
        public Guid CategoryId { get; set; }
        public Guid MovieId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        
    }
}
