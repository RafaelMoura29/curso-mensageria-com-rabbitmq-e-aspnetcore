using EasyNetQ;
using EasyNetQ.Marketing.API.Subscribers;

var builder = WebApplication.CreateBuilder(args);

var bus = RabbitHutch.CreateBus("host=localhost");
builder.Services.AddSingleton(bus);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<CustomerCreatedSubscriber>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
