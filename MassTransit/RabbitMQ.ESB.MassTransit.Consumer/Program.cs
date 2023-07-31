using MassTransit;
using RabbitMQ.ESB.MassTransit.Consumer.Consumers;

string rabbitMqUri = "amqps://xomvilok:0Ykb9rlf4FN8zSTZsdigfnH9nPe152zA@chimpanzee.rmq.cloudamqp.com/xomvilok";

string queueName = "example-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(f =>
{
    f.Host(rabbitMqUri);//bu hostan dınleme yap 

    f.ReceiveEndpoint(queueName: queueName,
         configureEndpoint: endpoint =>
        {
            endpoint.Consumer<ExampleMessageConsumer>();
        });
});

await bus.StartAsync();
Console.ReadLine();