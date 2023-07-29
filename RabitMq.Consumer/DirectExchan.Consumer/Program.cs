

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

ConnectionFactory factory = new();
factory.Uri = new("amqps://xomvilok:VrOOuyIz__ROOl7aAzUK6JkwwXlbvpB3@chimpanzee.rmq.cloudamqp.com/xomvilok");

using IConnection connection = factory.CreateConnection();
using IModel chanel = connection.CreateModel();

//1.Adım
chanel.ExchangeDeclare(exchange: "direct-exhange-example", type: ExchangeType.Direct); //bu publısıhrdakı ıle aynı olmalıdır 
//2.Adım
string quename = chanel.QueueDeclare().QueueName; //burada kendısı bır kuyruk olusturcak ısmınıde kendisi vericek

//3.Adım

chanel.QueueBind(
    queue: quename,
    exchange: "direct-exhange-example",
    routingKey: "direct-queue-example");


EventingBasicConsumer consomer = new(chanel);
chanel.BasicConsume(
    queue: quename,
    autoAck: true,
    consumer: consomer);

consomer.Received += (send, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();


//1.Adım: Publisher dakı exchange ile birebir aynı isim ve typ a sahip bır exchange tanımlanmalıdır!

//2.Adım: publisher tarafından routing keyde bulunan degerdekı kuyruga gonderılen mesajları kendı olusturdugumuz kuyruga gonderilen mesajları kendi olusturdugumuz kuyruga yönlendirerek tüketmemiz gerekmektedir. bununn ıcın oncelıkle bır kuyruk olusturulmalıdır 

//3.Adım:
