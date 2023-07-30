

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://xomvilok:OnlQLdjDE8_P5vs1MqVOyUMJKemAEEno@chimpanzee.rmq.cloudamqp.com/xomvilok");

using IConnection connection = factory.CreateConnection();
using IModel chanel = connection.CreateModel();

chanel.ExchangeDeclare(
    exchange: "topic-excahnge-example",
    type: ExchangeType.Topic);

Console.Write("Dinlenecek Topic i belirtin:");
string topi = Console.ReadLine();
string namequeue = chanel.QueueDeclare().QueueName;

chanel.QueueBind(
    queue: namequeue,
     exchange: "topic-excahnge-example",
      routingKey: topi
    );

EventingBasicConsumer consumer = new(chanel);

chanel.BasicConsume(
    queue: namequeue,
    autoAck: true,
    consumer: consumer);

consumer.Received += (send, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};


Console.Read();

