
using MassTransit;
using MassTransit.Configuration;
using RabbitMQ.ESB.MassTransit.Shared.Message;

string rabbitMqUri = "amqps://xomvilok:0Ykb9rlf4FN8zSTZsdigfnH9nPe152zA@chimpanzee.rmq.cloudamqp.com/xomvilok";

string queueName = "example-queue";

//consolda bus dan belırlenir faktorıy rabbıtmq uzerınden bır operasyon yapcaz deriz
IBusControl bus = Bus.Factory.CreateUsingRabbitMq(f =>
{
    f.Host(rabbitMqUri);
});

ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new($"{rabbitMqUri}/{queueName}"));

Console.Write("Göderilecek mesaj: ");

string message = Console.ReadLine();
//send tek kuruk tek hedefe gonderir
await sendEndpoint.Send<IMassage>(new ExampleMessage()
{
    Text = message
});


Console.ReadLine();