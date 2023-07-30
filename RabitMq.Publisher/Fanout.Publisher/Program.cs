



using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://xomvilok:i4sdYOrfbvJ0kfPUgouNyiw58U4PXGG6@chimpanzee.rmq.cloudamqp.com/xomvilok");

using IConnection connection = factory.CreateConnection();
using IModel chanel = connection.CreateModel();

chanel.ExchangeDeclare(exchange: "fanout-exchane-example", type: ExchangeType.Fanout);


while (true)
{
    Console.Write("Mesaj: ");
    string mesasgea = Console.ReadLine();
    for (int i = 0; i < 4; i++)
    {
        byte[] message = Encoding.UTF8.GetBytes($"{mesasgea} -{i}");
        chanel.BasicPublish(
            exchange: "fanout-exchane-example",
            routingKey: string.Empty, //fanoult ta routıng key onemsız dır cunku tum kuyruklara verıcektır bu degeri
            body: message
            );
    }
}


Console.Read();