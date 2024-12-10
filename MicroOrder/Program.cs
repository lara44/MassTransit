using MassTransit;
using MicroOrder.Consummer;
using MicroOrder.Messages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<ProductCreatedConsumer>();

    configure.UsingAmazonSqs((context, cfg) =>
    {
        cfg.Host(builder.Configuration["AWS:Region"], h =>
        {
            h.AccessKey(builder.Configuration["AWS:AccessKey"]);
            h.SecretKey(builder.Configuration["AWS:AccessSecret"]);
        });

        // Configura el endpoint de la cola existente
        cfg.ReceiveEndpoint("dev_prueba_sqs", e =>
        {
            e.ConfigureConsumeTopology = false; // Evita la configuraci�n autom�tica de la topolog�a
            e.PrefetchCount = 10;
            e.ConfigureConsumer<ProductCreatedConsumer>(context); // Vincula el consumidor
        });

        // Configura el nombre del tema asociado con el evento
        cfg.Message<ProductCreatedEvent>(x =>
        {
            x.SetEntityName("dev_prueba_sns"); // Nombre exacto del tema en SNS
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
