using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cinema.GateWay.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Cinema.GateWay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketsController : ControllerBase
    {
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDTO>> Post([FromBody] BasketDTO data, Guid basketId)
        {
            var basket = Database._Baskets.FirstOrDefault(x => x.BasketId == basketId);
            if (basket == null)
            {
                return NotFound($"No hay carrito");
            }

         
            MovieDTO Moviedata = null;
            ProductDTO Productdata = null;
            using (var Httpclient = new HttpClient())
            {
                var response = await Httpclient.GetStringAsync($"http://localhost:51591/moviecategories/{data.CategoryId}/{data.MovieId}");
                Moviedata = JsonConvert.DeserializeObject<MovieDTO>(response);
                var responseCafetery = await Httpclient.GetStringAsync($"http://localhost:57490/cafeterycategories/{data.ProductCategoryId}/{data.ProductId}");
                Productdata = JsonConvert.DeserializeObject<ProductDTO>(responseCafetery);

                var totalTickets = Moviedata.Price * data.QuantityOfTickets;
                var totalProducts = Productdata.Price * data.QuantityOfProducts;
                
                var totalresult = totalTickets + totalProducts;

                data.total = totalresult;
            }
            
            if (Moviedata == null)
            {
                return this.Ok("No existe la pelicula!");
            }

            try
            {
                var json = JsonConvert.SerializeObject(data);
                var factory = new ConnectionFactory
                {
                    HostName = "localhost",
                    Port = 5672
                };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare("payment-queue", false, false, false, null);
                        var body = Encoding.UTF8.GetBytes(json);
                        channel.BasicPublish(string.Empty, "payment-queue", null, body);
                    }
                }

                return Ok("Your verification code is: " + Guid.NewGuid() + "\n\n" + "   **** Details ****\n" +
                         "Basket id: "+ data.BasketId + "\n" +
                         "  ******* Movie details *******" + "\n" + 
                         "Movie: " + Moviedata.Name + "\n" +
                         "QuantityTickets: " + data.QuantityOfTickets + "\n" +
                         "Subtotal Movies: " + Moviedata.Price * data.QuantityOfTickets + "\n" +
                         "---------------------------------" + "\n" +
                         "  ******* Cafetery details *******" + "\n" +
                         "Product: " + Productdata.Name + "\n" +
                         "QuantityProducts: " + data.QuantityOfProducts + "\n" +
                         "Subtotal Cafetery: " + Productdata.Price * data.QuantityOfProducts + "\n" +
                         "---------------------------------" + "\n" +
                         "total: " + data.total + "\n");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
