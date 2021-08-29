using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cinema.GateWay.Models;
using Newtonsoft.Json;

namespace Cinema.GateWay.Services
{
    public class CafeteryService : ICafeteryService
    {
        private readonly HttpClient _httpClient;

        public CafeteryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CafeteryCategoryDTO>> GetCafeteryCategoriesAsync()
        {
            var result = await _httpClient.GetStringAsync("CafeteryCategories");
            return JsonConvert.DeserializeObject<IEnumerable<CafeteryCategoryDTO>>(result);
        }

        public async Task<CafeteryCategoryDTO> GetCafeteryCategoriesByIdAsync(Guid id)
        {
            var result = await _httpClient.GetStringAsync($"CafeteryCategories/{id}");
            return JsonConvert.DeserializeObject<CafeteryCategoryDTO>(result);
        }
    }
}
