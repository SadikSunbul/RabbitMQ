
//Consumer

using Consumer.Consumer;
using MassTransit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<ExampleMessageConsummer>();

            configurator.UsingRabbitMq((context, _configuretor) =>
            {
                _configuretor.Host("amqps://xomvilok:bzp8Z98qipQkPmCF5a26xxwwoE1ejUOh@chimpanzee.rmq.cloudamqp.com/xomvilok");

                _configuretor.ReceiveEndpoint("example-message-queue", e => e.ConfigureConsumer<ExampleMessageConsummer>(context));//bu kuyrugu dinle
            });
        });
    })
    .Build();

await host.RunAsync();
