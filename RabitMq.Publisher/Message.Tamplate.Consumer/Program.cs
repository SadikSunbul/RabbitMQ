
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

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

//byte[] message = Encoding.UTF8.GetBytes("Merhaba");
//chanel.BasicPublish(
//    exchange: string.Empty, //burası bos ıse buranın default değeri Direct dir
//    routingKey: queueName,
//    body: message
//    );
#endregion


#region Publish/Subscrite(Pub/Sub)Tasarımı

//string exchangeName = "example-pub-sub-exchange";

//chanel.ExchangeDeclare(
//    exchange: exchangeName,
//    type: ExchangeType.Fanout);

//byte[] message = Encoding.UTF8.GetBytes("merhaba");

//chanel.BasicPublish(
//       exchange: exchangeName,
//        routingKey: string.Empty, //tum kuyruklara gonderecegı ıcın bos 
//         body: message
//     );

#endregion


#region Work Queue(İş Kuyruğu) Tasarımı​
//string queueName = "example-work-queue";

//channel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false);

//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(200);

//    byte[] message = Encoding.UTF8.GetBytes("merhaba" + i);

//    channel.BasicPublish(
//        exchange: string.Empty,
//        routingKey: queueName,
//        body: message);
//}

#endregion
#region Request/Response Tasarımı​
string requestQueueName = "example-request-response-queue";
chanel.QueueDeclare(
    queue: requestQueueName,
    durable: false,
exclusive: false,
    autoDelete: false);

string replyQueueName = chanel.QueueDeclare().QueueName; //rrquest kuyrugu

string correlationId = Guid.NewGuid().ToString();

#region Request Mesajını Oluşturma ve Gönderme
IBasicProperties properties = chanel.CreateBasicProperties();
properties.CorrelationId = correlationId; //KOntrol ıcın gonderılen ıd buradanmmı geldi diye
properties.ReplyTo = replyQueueName; //burası gondeerılen mesaj mahıyetınde beklenıcek responsun hangı kuyruga gonderılcegını belirtir

for (int i = 0; i < 10; i++)
{
    byte[] message = Encoding.UTF8.GetBytes("merhaba" + i);
    chanel.BasicPublish(
        exchange: string.Empty,
        routingKey: requestQueueName,
        body: message,
        basicProperties: properties);
}
#endregion
#region Response Kuyruğu Dinleme
EventingBasicConsumer consumer = new(chanel);
chanel.BasicConsume(
    queue: replyQueueName,
    autoAck: true,
    consumer: consumer);

consumer.Received += (sender, e) =>
{
    if (e.BasicProperties.CorrelationId == correlationId)
    {
        //....
        Console.WriteLine($"Response : {Encoding.UTF8.GetString(e.Body.Span)}");
    }
};
#endregion

#endregion

Console.ReadLine();