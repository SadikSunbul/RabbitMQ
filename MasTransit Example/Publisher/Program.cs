
//Publisher

using MassTransit;
using Publisher.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(configurator =>
        {
            configurator.UsingRabbitMq((context, _configuretor) =>
            {
                _configuretor.Host("amqps://xomvilok:bzp8Z98qipQkPmCF5a26xxwwoE1ejUOh@chimpanzee.rmq.cloudamqp.com/xomvilok");
            });
        });

        services.AddHostedService<PublisMessageService>(provider =>
        {
            using IServiceScope scope = provider.CreateScope();

            IPublishEndpoint publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();

            return new PublisMessageService(publishEndpoint);
        });
    })
    .Build();

host.Run();
