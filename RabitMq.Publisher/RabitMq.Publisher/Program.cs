


using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();

//Bağlantı olusturma kısmı
factory.Uri = new("amqps://xomvilok:VrOOuyIz__ROOl7aAzUK6JkwwXlbvpB3@chimpanzee.rmq.cloudamqp.com/xomvilok");


//Bağlantıyı aktifleştirme ve kanal açma

using IConnection connection = factory.CreateConnection(); //aktıflestırme
using IModel chanel = connection.CreateModel(); //kanal olusturma 


//Queue Oluşturma
//kanal ustunden kutuk olusturulur
chanel.QueueDeclare(queue: "example-queue",exclusive:false,durable:true);
//exclusive true olursa bu baglantının dısında baska bır baglantı bu kuyrugu kullanamıycaktır false yapmaz ısek consumer kısmından bu kuyruga baglanamayız ve bu kuyruk sılınır 


//Queuya mesaj gönderme

//RabbitMQ kuyruga atıcagı mesajları byte türünden kabul etmektedir.Haliyle mesajları bizim byte dönüştürmemiz gerekecektir

//byte[] message=Encoding.UTF8.GetBytes("Merhaba"); //merhaba byta dondururldü 

//chanel.BasicPublish(exchange:"",routingKey: "example-queue",body:message);
//routingKey: "example-queue" içindeki isimli kuyruga ekle dedik
//exchange:"" bunu bildirmediğimiz için bu directoryi kabul edecektir ve routingKey e verilen kuyrga ılgılı degerı gonderecektir

IBasicProperties properties=chanel.CreateBasicProperties();
properties.Persistent = true;

for (int i = 0; i < 500; i++)
{
    await Task.Delay(100);//bu kadar bekle dedik
    byte[] message = Encoding.UTF8.GetBytes("Merhaba"+i); //merhaba byta dondururldü 

    chanel.BasicPublish(exchange: "", routingKey: "example-queue", body: message,basicProperties: properties);
}
Console.Read();
