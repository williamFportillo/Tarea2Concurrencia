using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.GateWay.Models
{
    public class BasketDTO
    {
        public Guid BasketId { get; set; }

        public Guid CategoryId { get; set; }
        public Guid MovieId { get; set; }

        public Guid ProductCategoryId { get; set; }
        public Guid ProductId { get; set; }

        public int QuantityOfTickets { get; set; }

        public int QuantityOfProducts { get; set; }
        public decimal total { get; set; }
    }
}
