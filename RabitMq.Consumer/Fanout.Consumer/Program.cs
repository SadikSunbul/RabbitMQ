

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Numerics;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();

factory.Uri = new("amqps://xomvilok:i4sdYOrfbvJ0kfPUgouNyiw58U4PXGG6@chimpanzee.rmq.cloudamqp.com/xomvilok");

using IConnection connection = factory.CreateConnection();
using IModel chanel = connection.CreateModel();

chanel.ExchangeDeclare(exchange: "fanout-exchane-example", type: ExchangeType.Fanout);

Console.Write("Kuyruk adını giriniz:");
string _queuName = Console.ReadLine();

chanel.QueueDeclare(queue: _queuName, exclusive: false);//bu isimde bir kuyruk olusturduk

chanel.QueueBind( //exchange ıle kuyrukların ılıskılendırılmesı ıslemı 
     queue: _queuName,
      exchange: "fanout-exchane-example",
      routingKey: string.Empty
    );

EventingBasicConsumer consumer = new(chanel);
chanel.BasicConsume(
    queue: _queuName,
    autoAck: true,
     consumer: consumer
    );

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};


Console.Read();
