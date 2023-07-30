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


while (true)
{
    Console.Write("Mesaj");
    string ms = Console.ReadLine();
    byte[] message = Encoding.UTF8.GetBytes(ms);
    Console.Write("Topic giriniz:");
    string topic = Console.ReadLine();
    chanel.BasicPublish(
        exchange: "topic-excahnge-example",
        routingKey: topic, //routıng key exchange turune gore rol alamaktadır yanı topıc oluyor burası
        body: message
        );
}


Console.Read();