using MassTransit;
using MicroProduct.Messages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(configure =>
{
    configure.UsingAmazonSqs((context, cfg) =>
    {
        cfg.Host(builder.Configuration["AWS:Region"], h =>
        {
            h.AccessKey(builder.Configuration["AWS:AccessKey"]);
            h.SecretKey(builder.Configuration["AWS:AccessSecret"]);
            h.Scope("dev", true);
        });

        cfg.Message<ProductCreatedEvent>(x =>
        {
            x.SetEntityName("dev_prueba_sns");
        });

        // No configura colas, solo publica en el tema existente
        cfg.ConfigureEndpoints(context, new DefaultEndpointNameFormatter("dev-", false));
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
