using EasyNetQ;
using Newtonsoft.Json;

const string EXCHANGE = "curso-rabbitmq";
const string QUEUE = "person-created";
const string ROUTING_KEY = "hr.person-created";

var person = new Person("Rafael", "123.456.789-10", new DateTime(2000, 3, 29));

var bus = RabbitHutch.CreateBus("host=localhost");
var advanced = bus.Advanced;

var exchange = advanced.ExchangeDeclare(EXCHANGE, "topic");
var queue = advanced.QueueDeclare(QUEUE);

advanced.Publish(exchange, ROUTING_KEY, true, new Message<Person>(person));

advanced.Consume<Person>(queue, (msg, info) =>
{
    var json = JsonConvert.SerializeObject(msg.Body);
    Console.WriteLine(json);
});

// await bus.PubSub.PublishAsync(person);

// await bus.PubSub.SubscribeAsync<Person>("marketing", msg => {
//     var json = JsonConvert.SerializeObject(msg);
//     Console.WriteLine(json);
// });

Console.ReadLine();