using System;
using System.Collections.Generic;
using Cinema.GateWay.Models;

namespace Cinema.GateWay
{
    public static class Database
    {
        public static readonly List<BasketDTO> _Baskets = new List<BasketDTO>
        {
            new BasketDTO
            {
                BasketId = new Guid("cab7517b-2b8e-4f67-a4ab-ba64bf331a20"),
                QuantityOfProducts = 0,
                QuantityOfTickets = 0,
                total = 0
            }
        };
    }
}
