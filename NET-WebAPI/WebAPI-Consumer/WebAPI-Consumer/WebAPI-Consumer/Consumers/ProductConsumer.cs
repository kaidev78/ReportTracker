using System;
using System.Threading.Tasks;
using MassTransit;
using WebAPI_Publisher.Models;

namespace WebAPI_Consumer.Consumers
{
    public class ProductConsumer : IConsumer<Product>
    {
        public async Task Consume(ConsumeContext<Product> context)
        {
            var data = context.Message;
            await Console.Out.WriteAsync(context.Message.ProductName);
        }
    }
}
