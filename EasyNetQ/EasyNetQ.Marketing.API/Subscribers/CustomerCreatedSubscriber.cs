using EasyNetQ;
using Newtonsoft.Json;

namespace EasyNetQ.Marketing.API.Subscribers
{
    public class CustomerCreatedSubscriber : IHostedService
    {
        private readonly IAdvancedBus _bus;
        public IServiceProvider Services { get; }
        const string EXCHANGE = "curso-rabbitmq";
        const string CUSTOMER_CREATED_QUEUE = "customer-created";

        public CustomerCreatedSubscriber(IServiceProvider services, IBus bus)
        {
            _bus = bus.Advanced;
            Services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var queue = _bus.QueueDeclare(CUSTOMER_CREATED_QUEUE);

            _bus.Consume<CustomerCreated>(queue, (msg, info) =>
            {
                var json = JsonConvert.SerializeObject(msg.Body);
                Console.WriteLine(json);
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}