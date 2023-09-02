using _3_Shared.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Consumer;

public class ExampleMessageConsummer : IConsumer<IMassage>
{
    public Task Consume(ConsumeContext<IMassage> context)
    {
        Console.WriteLine($"Gelen Mesaj:{context.Message.Text}");
        return Task.CompletedTask;
    }
}
