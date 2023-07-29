
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();

//  Consumer


//Bağlantı oluşturma
factory.Uri = new("amqps://xomvilok:VrOOuyIz__ROOl7aAzUK6JkwwXlbvpB3@chimpanzee.rmq.cloudamqp.com/xomvilok");

//Bağlantı aktifleştirme ve kanal Açma 
using IConnection connect = factory.CreateConnection();

using IModel chanel = connect.CreateModel();

//Queue Oluşturma
chanel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true); //buradakı deger publisher dan kopyalayıp ldık aynı degerlerın olması gerek

//Queue den mesaj okuma 

EventingBasicConsumer consumer = new(chanel); //event tanımlanmalı okuma ıslemı ıcın 

chanel.BasicConsume(queue: "example-queue", autoAck: false, consumer);
//autoAck:false alınan degerın sılınıp sılınmıycgını ıfade eder false deıgımız ıcın consumerdan onay almadıgı surece sılmez degeri

chanel.BasicQos(0,1,false); //burada servise esıt dagılım sevkıyat yapar tutarlı bısekılde esıt tempo ıle calısır
consumer.Received += (sender, e) =>
{
    //Kuyruga gelen mesajın işlendiği yerdir!
    // e.Body:kuyruktakı mesajın verısını butunsel oolarak getırcektır
    //e.Body.Span veya e.Body.ToArray() :kuyruktaki mesajın byte verısını getircektir


    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));//byte dizisini strınge dondurduk 

    //işlemler başarılı tamamlandı ise artık rabbitMQ daki veri silincektir altaki kod ile birlikte
    chanel.BasicAck(deliveryTag:e.DeliveryTag,multiple:false);
    //multiple:false sadece bu  mesajı sıler true dersek bundan önceki tum mesajlar sıler bu ıstemedıgımız bır durumdur bu proje ıcın 

    //buradakı kodu yazmaz ısek yenıden baslatırsak burayı silmediği için kuyrukta duruyor veriler ama söyleki default deeğri 30 dk 30 dk sonra kuyruga bu verıler yenıden eklenicek silinmediği için 30 dk sonra bu verıeler yenıden gelicektir buraya

};


Console.Read();

