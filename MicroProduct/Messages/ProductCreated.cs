using MassTransit;

namespace MicroProduct.Messages
{
    [MessageUrn("ProductCreatedEvent")]
    public class ProductCreatedEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
