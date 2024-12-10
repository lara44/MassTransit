using MassTransit;
using MicroProduct.Messages;
using Microsoft.AspNetCore.Mvc;

namespace MicroProduct.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class PruebaController : Controller
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PruebaController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("prueba")]
        public async Task<ActionResult<Product>> Index()
        {
            // Crear el producto
            var product = new Product(1, "Arroz");

            // Publicar el mensaje en MassTransit
            var productMessage = new ProductCreatedEvent
            {
                Id = product.Id,
                Name = product.Name
            };

            await _publishEndpoint.Publish(productMessage);

            // Retornar el objeto con un código de estado HTTP 200
            return Ok(product);
        }
    }
}

public class Product {
    public int Id { get; set; }
    public string? Name { get; set; }

    public Product(int id, String name) { 
        Id = id;
        Name = name;
    }
}
