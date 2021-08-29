using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Movies.Models;

namespace Cinema.Movies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieCategoriesController : ControllerBase
    {
        private static readonly List<CategoryDTO> Categories = new List<CategoryDTO>
        {
            new CategoryDTO
            {
                CategoryId = new Guid("20983d00-f2ae-4361-bdad-68f14e46a32c"),
                Name = "Horror",
            },
            new CategoryDTO
            {
                CategoryId = new Guid("7eeb2e1f-8648-4ea4-a339-f7005671deae"),
                Name = "Comedy"
            }
        };

        private static readonly List<MovieDTO> Movies = new List<MovieDTO>
        {
            new MovieDTO
            {
                CategoryId = new Guid("20983d00-f2ae-4361-bdad-68f14e46a32c"),
                MovieId = new Guid("e7145866-3b9d-437a-9ef8-a8aaf2d98423"),
                Name = "Halloween",
                Description = "Que miedo!",
                Price = 100
            },

            new MovieDTO
            {
                CategoryId = new Guid("20983d00-f2ae-4361-bdad-68f14e46a32c"),
                MovieId = new Guid("c5a37aaa-92b4-4c7d-aae7-877e084a5d12"),
                Name = "El aro",
                Description = "Va a estar potente",
                Price = 120
            },

            new MovieDTO
            {
                CategoryId = new Guid("7eeb2e1f-8648-4ea4-a339-f7005671deae"),
                MovieId = new Guid("f6695b67-8138-43c2-8cb2-dbe1db9d09c0"),
                Name = "Son como niños",
                Description = "jaja",
                Price = 85
            },
            new MovieDTO
            {
                CategoryId = new Guid("7eeb2e1f-8648-4ea4-a339-f7005671deae"),
                MovieId = new Guid("04bbf03d-02f5-4851-823b-34c4bf798d59"),
                Name = "American Pie",
                Description = "Cague de risa",
                Price = 95
            }
        };
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> Get()
        {
            return this.Ok(Categories);
        }

        [HttpGet("{categoryId}")]
        public ActionResult<IEnumerable<CategoryDTO>> Get(Guid categoryId)
        {
            var category = Categories.FirstOrDefault(x => x.CategoryId == categoryId);

            if (category == null)
            {
                return NotFound($"No se encontro una categoria con el id {categoryId}");
            }

            var result = Movies.Where(x => x.CategoryId == categoryId).ToList();
            return Ok(result);
        }

        [HttpGet("{categoryId}/{movieId}")]
        public ActionResult<MovieDTO> Get(Guid categoryId,Guid movieId)
        {
            var category = Categories.FirstOrDefault(x => x.CategoryId == categoryId);

            if (category == null)
            {
                return NotFound($"No se encontro una categoria con el id {categoryId}");
            }

            var result = Movies.FirstOrDefault(x => x.MovieId == movieId);
            return Ok(result);
        }


    }
}
