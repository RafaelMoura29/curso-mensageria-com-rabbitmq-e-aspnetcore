using MessagingEvents.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace MassTransit.Marketing.Subscribers
{
   public class CustomerCreatedSubscriber : IConsumer<CustomerCreated>
    {
        public IServiceProvider ServiceProvider { get;}
        public CustomerCreatedSubscriber(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public async Task Consume(ConsumeContext<CustomerCreated> context)
        {
            var @event = context.Message;

            Console.WriteLine(@event.FullName);
        }
    }
}