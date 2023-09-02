
//Publisher

using _3_Shared.Messages;
using MassTransit;
using MassTransit.Configuration;

string rabbitMQUri = "amqps://xomvilok:bzp8Z98qipQkPmCF5a26xxwwoE1ejUOh@chimpanzee.rmq.cloudamqp.com/xomvilok";

string queueName = "example-queuewqe";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new($"{rabbitMQUri}/{queueName}")); //send tek bir kuyruga mesaj göndermemizi sağlar 

Console.Write("Gönderilecek mesaj: ");
string message = Console.ReadLine();
await sendEndpoint.Send<IMassage>(new ExampleMessage()
{
    Text = message
});

Console.ReadKey();
