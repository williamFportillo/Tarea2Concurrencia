using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.GateWay.Models;

namespace Cinema.GateWay.Services
{
    public interface IMoviesService
    {
        Task<IEnumerable<CategoryDTO>> GetAsync();

        Task<IEnumerable<MovieDTO>> GetByIdAsync(Guid id);
    }
}
