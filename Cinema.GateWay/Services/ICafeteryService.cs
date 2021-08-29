using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.GateWay.Models;

namespace Cinema.GateWay.Services
{
    public interface ICafeteryService
    {
        Task<IEnumerable<CafeteryCategoryDTO>> GetCafeteryCategoriesAsync();

        Task<CafeteryCategoryDTO> GetCafeteryCategoriesByIdAsync(Guid id);
    }
}
