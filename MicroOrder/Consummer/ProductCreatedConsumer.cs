using MassTransit;
using MicroOrder.Messages;

namespace MicroOrder.Consummer
{
    public class ProductCreatedConsumer : IConsumer<ProductCreatedEvent>
    {
        private readonly ILogger<ProductCreatedConsumer> _logger;
        public ProductCreatedConsumer(ILogger<ProductCreatedConsumer> logger) {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            var product = context.Message;
            _logger.LogInformation($"Producto recibido: {product.Name}");
            return Task.CompletedTask;
        }
    }
}
