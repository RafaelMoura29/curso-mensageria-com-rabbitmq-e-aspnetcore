namespace MassTransit.Customers.Bus
{
    public interface IBusService
    {
        Task Publish<T>(T message);
    }
}