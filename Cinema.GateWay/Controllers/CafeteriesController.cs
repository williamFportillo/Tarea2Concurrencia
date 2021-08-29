using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.GateWay.Models;
using Cinema.GateWay.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.GateWay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CafeteriesController : ControllerBase
    {
        private readonly ICafeteryService _cafeteryService;

        public CafeteriesController(ICafeteryService cafeteryService )
        {
            _cafeteryService = cafeteryService;
        }

        [HttpGet("cafeterycategories")]
        public async Task<ActionResult<CafeteryCategoryDTO>> Get()
        {
            var result = await _cafeteryService.GetCafeteryCategoriesAsync();
            return Ok(result);
        }

        [HttpGet("cafeterycategories/{id}")]
        public async Task<ActionResult<CafeteryCategoryDTO>> GetById(Guid id)
        {
            var result = await _cafeteryService.GetCafeteryCategoriesByIdAsync(id);
            return Ok(result);
        }
    }
}
