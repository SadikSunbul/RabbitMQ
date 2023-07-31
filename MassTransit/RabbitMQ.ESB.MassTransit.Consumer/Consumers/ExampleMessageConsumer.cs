using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.ESB.MassTransit.Consumer.Consumers
{
    public class ExampleMessageConsumer : IConsumer<IMassage>
    {
        public Task Consume(ConsumeContext<IMassage> context)
        {
            Console.WriteLine($"gelen mesaj:{context.Message.Text}");
            return Task.CompletedTask;
        }
    }
}
