using RabbitMQ.Client;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace RabbitMQClient.Customers.API.Bus
{
    public class RabbitMqClientService : IBusService
    {
        const string EXCHANGE = "curso-rabbitmq";
        private readonly IModel _channel;

        public RabbitMqClientService()
        {
            var connectionFactory = new ConnectionFactory 
            {
                HostName = "localhost"
            };

            var connection = connectionFactory.CreateConnection("curso-rabbitmq-client-publisher");

            _channel = connection.CreateModel();
        }

        public void Publish<T>(string routingKey, T message)
        {
            var json = JsonSerializer.Serialize(message);

            var byteArray = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(EXCHANGE, routingKey, null, byteArray);
        }
    }
}
