


using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri =new("amqps://xomvilok:VrOOuyIz__ROOl7aAzUK6JkwwXlbvpB3@chimpanzee.rmq.cloudamqp.com/xomvilok");

using IConnection connection = factory.CreateConnection();
using IModel chanel = connection.CreateModel();

//DirectExchange

chanel.ExchangeDeclare(exchange: "direct-exhange-example", type: ExchangeType.Direct);

while (true)
{
    Console.WriteLine("Mesaj:");
    string mesage = Console.ReadLine();
    byte[] byteMessage=Encoding.UTF8.GetBytes(mesage);

    chanel.BasicPublish(
        exchange: "direct-exhange-example", //usteki exchange ısmını aldık 
        routingKey: "direct-queue-example", //kuruk adı 
        body:byteMessage);
}


Console.Read();