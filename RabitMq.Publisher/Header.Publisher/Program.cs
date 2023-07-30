


using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://xomvilok:66RBb7gvXkydDqRrCN01h48xkNHXCyxW@chimpanzee.rmq.cloudamqp.com/xomvilok");

using IConnection connection = factory.CreateConnection();
using IModel chanel = connection.CreateModel();

chanel.ExchangeDeclare(
    exchange: "header-exchange-example",
    type: ExchangeType.Headers);

while (true)
{
    Console.Write("Message : ");
    string mesg = Console.ReadLine();
    byte[] message = Encoding.UTF8.GetBytes(mesg);
    Console.WriteLine("Lutfen Header value sını gırını:");
    string value=Console.ReadLine();

    IBasicProperties basicProperties = chanel.CreateBasicProperties();
    basicProperties.Headers = new Dictionary<string,object>
    {
        ["no"]=value
    };

    chanel.BasicPublish(
         exchange: "header-exchange-example",
         routingKey:string.Empty,
         body:message,
         basicProperties: basicProperties
        );
}



Console.Read();