using _3_Shared.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_Consumer.Consumers;

public class ExampleMessageConsumer : IConsumer<IMassage> //ilgili kuytruga bu turden bır mesaj gelirse (IMassage) burasoı tetıklenıcektır
{
    public Task Consume(ConsumeContext<IMassage> context)
    {
        Console.WriteLine($"Gelen mesaj {context.Message.Text}");

        return Task.CompletedTask;
    }
}
