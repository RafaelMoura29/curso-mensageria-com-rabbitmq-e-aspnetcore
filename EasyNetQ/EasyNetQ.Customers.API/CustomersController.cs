using MessagingEvents.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using EasyNetQ.Customers.API.Bus;

namespace EasyNetQ.Customers.API
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        const string ROUTING_KEY = "customer-created";
        private readonly IBusService _bus;

        public CustomersController(IBusService bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public IActionResult Post(CustomerInputModel model)
        {
            var @event = new CustomerCreated(model.Id, model.FullName, model.Email, model.PhoneNumber, model.BirthDate);

            _bus.Publish(ROUTING_KEY, @event);
            return NoContent();
        }
    }
}