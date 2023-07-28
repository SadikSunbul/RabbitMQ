
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();


//Bağlantı oluşturma
factory.Uri = new("amqps://xomvilok:VrOOuyIz__ROOl7aAzUK6JkwwXlbvpB3@chimpanzee.rmq.cloudamqp.com/xomvilok");

//Bağlantı aktifleştirme ve kanal Açma 
using IConnection connect = factory.CreateConnection();

using IModel chanel = connect.CreateModel();

//Queue Oluşturma
chanel.QueueDeclare(queue: "example-queue", exclusive: false); //buradakı deger publisher dan kopyalayıp ldık aynı degerlerın olması gerek

//Queue den mesaj okuma 

EventingBasicConsumer consumer = new(chanel); //event tanımlanmalı okuma ıslemı ıcın 

chanel.BasicConsume(queue: "example-queue", autoAck: false, consumer);
//autoAck:false alınan degerın sılınıp sılınmıycgını ıfade eder
consumer.Received += (sender, e) =>
{
    //Kuyruga gelen mesajın işlendiği yerdir!
    // e.Body:kuyruktakı mesajın verısını butunsel oolarak getırcektır
    //e.Body.Span veya e.Body.ToArray() :kuyruktaki mesajın byte verısını getircektir


    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));//byte dizisini strınge dondurduk 
};


Console.Read();
