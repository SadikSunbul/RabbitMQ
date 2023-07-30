


using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://xomvilok:lq4WO_gs4cNCDbQY6jh_1u7O8Up8Muxe@chimpanzee.rmq.cloudamqp.com/xomvilok");

using IConnection connection = factory.CreateConnection();
using IModel chanel = connection.CreateModel();

#region P2P (Point-To-Point) Tasarımı
//string queueName = "example-p2p-queue";
//chanel.QueueDeclare( //kurugu olusturduk
//    queue: queueName,
//     durable: false,
//     exclusive: false);

//EventingBasicConsumer consumer = new(chanel);
//chanel.BasicConsume(
//    queue: queueName,
//    autoAck: false,
//    consumer: consumer);
//consumer.Received += (send, e) =>
//{
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//};
#endregion

#region Publish/Subscrite(Pub/Sub)Tasarımı

#endregion
#region Work Queue(iş kuyrugu)Tasarımı

#endregion
#region Request/Response Tasarımı

#endregion

Console.ReadLine();