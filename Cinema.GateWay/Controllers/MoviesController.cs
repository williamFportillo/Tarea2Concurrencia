using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.GateWay.Models;
using Cinema.GateWay.Services;

namespace Cinema.GateWay.Controllers
{
    [ApiController]
    [Route("[controller]")]

    
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        [HttpGet("moviecategories")]
        public async Task<ActionResult<CategoryDTO>> Gets()
        {
            var result = await _moviesService.GetAsync();
            return Ok(result);
        }

        [HttpGet("moviecategories/{id}")]
        public async Task<ActionResult<MovieDTO>> GetById(Guid id)
        {
            var result = await _moviesService.GetByIdAsync(id);
            return Ok(result);
        }
    }
}
