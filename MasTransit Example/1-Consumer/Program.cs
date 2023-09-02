
//Consumer

using _1_Consumer.Consumers;
using MassTransit;

string rabbitMQUri = "amqps://xomvilok:bzp8Z98qipQkPmCF5a26xxwwoE1ejUOh@chimpanzee.rmq.cloudamqp.com/xomvilok";


string queueName = "example-queuewqe";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri); //Buradan dinleme yap 

    factory.ReceiveEndpoint(queueName, endpoint =>
    { //buradakı kuyruğa bir mesaj gelirse ExampleMessageConsumer u çalıştır
        endpoint.Consumer<ExampleMessageConsumer>();
    });
});

await bus.StartAsync(); //bunu yapmazsak consumer calısmaz 

Console.ReadLine();
