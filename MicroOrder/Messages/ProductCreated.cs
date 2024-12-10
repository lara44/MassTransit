using MassTransit;

namespace MicroOrder.Messages
{
    [MessageUrn("ProductCreatedEvent")]
    public class ProductCreatedEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
