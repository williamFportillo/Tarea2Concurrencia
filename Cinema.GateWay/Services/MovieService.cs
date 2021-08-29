using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Cinema.GateWay.Models;
using Newtonsoft.Json;

namespace Cinema.GateWay.Services
{
    public class MovieService : IMoviesService
    {
        private readonly HttpClient _httpClient;

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CategoryDTO>> GetAsync()
        {
            var categories = await _httpClient.GetStringAsync("moviecategories");
            return JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(categories);

        }

        public async Task<IEnumerable<MovieDTO>> GetByIdAsync(Guid id)
        {
            var category = await _httpClient.GetStringAsync($"Moviecategories/{id}");
            return JsonConvert.DeserializeObject<IEnumerable<MovieDTO>>(category);
        }
    }
}
