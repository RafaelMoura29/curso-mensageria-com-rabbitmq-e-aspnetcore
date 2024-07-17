using MessagingEvents.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using MassTransit.Customers.Bus;
using System;
namespace MassTransit.Customers.Controllers
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
        public async Task<IActionResult> Post(CustomerInputModel model)
        {
            var @event = new CustomerCreated(model.Id, model.FullName, model.Email, model.PhoneNumber, model.BirthDate);

            await _bus.Publish(@event);

            return NoContent();
        }
    }
}