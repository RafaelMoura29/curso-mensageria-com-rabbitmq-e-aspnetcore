using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

const string EXCHANGE = "curso-rabbitmq";

var person = new Person("Rafael", "123.456.789-10", new DateTime(2000, 3, 29));

var connectionFactory = new ConnectionFactory
{
    HostName = "localhost"
};

var connection = connectionFactory.CreateConnection("curso-rabbitmq");

var channel = connection.CreateModel();

var json = JsonSerializer.Serialize(person);
var byteArray = Encoding.UTF8.GetBytes(json);

channel.BasicPublish(EXCHANGE, "hr.person-created", null, byteArray);

Console.WriteLine($"Message published: {json}");

var consumerChannel = connection.CreateModel();

var consumer = new EventingBasicConsumer(consumerChannel);

consumer.Received += async (sender, EventArgs) =>
{
    var contentArray = EventArgs.Body.ToArray();
    var contentString = Encoding.UTF8.GetString(contentArray);

    var message = JsonSerializer.Deserialize<Person>(contentString);

    Console.WriteLine($"Message received: {contentString}");

    consumerChannel.BasicAck(EventArgs.DeliveryTag, false);
};

consumerChannel.BasicConsume("person-created", false, consumer);

Console.ReadLine();
