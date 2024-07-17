using EasyNetQ;
using EasyNetQ.Customers.API.Bus;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var bus = RabbitHutch.CreateBus("host=localhost");

builder.Services.AddSingleton<IBusService, EasyNetQService>(
    o => new EasyNetQService(bus)
);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
