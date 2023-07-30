


using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

ConnectionFactory factory = new();
factory.Uri = new("amqps://xomvilok:66RBb7gvXkydDqRrCN01h48xkNHXCyxW@chimpanzee.rmq.cloudamqp.com/xomvilok");

using IConnection connection = factory.CreateConnection();
using IModel chanel = connection.CreateModel();

chanel.ExchangeDeclare(
    exchange: "header-exchange-example",
    type: ExchangeType.Headers);

Console.Write("Lutfenn header valuesini giriniz:");
string value = Console.ReadLine();

string queuName = chanel.QueueDeclare().QueueName;

chanel.QueueBind(
     queue: queuName,
     exchange: "header-exchange-example",
     routingKey: string.Empty,
      new Dictionary<string, object>
      {
          ["x-match"]="all",//default tu any dir any dediğimizde sadece bu anahtarı içerenlere gönderiri
          ["no"] = value
      }
    );

EventingBasicConsumer consumer = new(chanel);

chanel.BasicConsume(
    queue: queuName,
     autoAck: true,
     consumer: consumer
    );

consumer.Received += (send, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};


Console.Read();