using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Cafetery.Models;

namespace Cinema.Cafetery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CafeteryCategoriesController : ControllerBase
    {
        private static readonly List<CategoryDTO> Categories = new List<CategoryDTO>
        {
            new CategoryDTO
            {
                CategoryId = new Guid("c928276b-bcfd-400e-a28e-bcdedc713c7a"),
                Name = "Salty",
                Products = new List<ProductDTO>
                {
                    new ProductDTO
                    {
                        CategoryId = new Guid("c928276b-bcfd-400e-a28e-bcdedc713c7a"),
                        ProductId = new Guid("31a95ad8-75fc-4aca-8a28-ea7bc90b2fbf"),
                        Name = "Pop Corn",
                        Price = 50
                    },
                    new ProductDTO
                    {
                        CategoryId = new Guid("c928276b-bcfd-400e-a28e-bcdedc713c7a"),
                        ProductId = new Guid("0b3edd6b-e7be-40c3-b208-522ee9486c27"),
                        Name = "Nacho cheese",
                        Price = 40
                    }
                }
            },
            new CategoryDTO
            {
                CategoryId = new Guid("8b149f25-3f34-4cc6-b730-63751c3c00dd"),
                Name = "Sweet",
                Products = new List<ProductDTO>
                {
                    new ProductDTO
                    {
                        CategoryId = new Guid("8b149f25-3f34-4cc6-b730-63751c3c00dd"),
                        ProductId = new Guid("a4dcdfad-8c6c-4e43-88b0-4d7890dd957f"),
                        Name = "Skittles",
                        Price = 80
                    },
                    new ProductDTO
                    {
                        CategoryId = new Guid("8b149f25-3f34-4cc6-b730-63751c3c00dd"),
                        ProductId = new Guid("688f1de4-35d8-49ec-a2e7-cd46ad3dae7c"),
                        Name = "Gummies",
                        Price = 30
                    }
                }
            },
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
            ;
            return Ok(category);
        }

        [HttpGet("{categoryId}/{productId}")]
        public ActionResult<ProductDTO> Get(Guid categoryId, Guid productId)
        {
            var category = Categories.FirstOrDefault(x => x.CategoryId == categoryId);

            if (category == null)
            {
                return NotFound($"No se encontro una categoria con el id {categoryId}");
            }

            var result = category.Products.FirstOrDefault(x => x.ProductId == productId);
            return Ok(result);
        }
    }
}
