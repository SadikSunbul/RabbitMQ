using _3_Shared.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisher.Services;

public class PublisMessageService : BackgroundService
{
    readonly IPublishEndpoint _publishEndpoint;

    public PublisMessageService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int i = 0;
        while (true)
        {
            ExampleMessage message = new()
            {
                Text = $"{i++}. mesaj"
            };
            //Kuyruk bildirmedik çünkü tüm kuyruklara mesajı gönderir
            await _publishEndpoint.Publish(message);
        }
    }
}
