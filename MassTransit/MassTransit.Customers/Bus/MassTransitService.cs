namespace MassTransit.Customers.Bus
{
    public class MassTransitService : IBusService
    {
        private readonly IBus _bus;

        public MassTransitService(IBus bus)
        {
            _bus = bus;

        }

        public async Task Publish<T>(T message)
        {
            await _bus.Publish(message);
        }
    }
}