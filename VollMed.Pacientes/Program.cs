using MassTransit;
using MassTransit.Configuration;
using Microsoft.EntityFrameworkCore;
using VollMed.Pacientes.Consumers;
using VollMed.Pacientes.Data;
using VollMed.Pacientes.Data.Repositories;
using VollMed.Pacientes.Domain.Interfaces;
using VollMed.Pacientes.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add DbContext
builder.Services.AddDbContext<VollMedDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

// Add Repositories
builder.Services.AddTransient<IPacienteRepository, PacienteRepository>();

// Add OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.RegisterConsumer<ReceitaCriadaConsumer>();
    x.UsingRabbitMq((ctx, cfg) =>
    {
        var rabbitMqConfig = builder.Configuration.GetSection("RabbitMQ");

        cfg.Host(rabbitMqConfig["Host"], Convert.ToUInt16(rabbitMqConfig["Port"]), "/", h =>
        {
            h.Username(rabbitMqConfig["Username"]!);
            h.Password(rabbitMqConfig["Password"]!);
        });

        cfg.ReceiveEndpoint("pacientes-receita-criada-event", e => e.ConfigureConsumer<ReceitaCriadaConsumer>(ctx));
    });
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    // Seed the database with initial data
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<VollMedDbContext>();
    DbSeeder.Seed(context);
}

app.UseHttpsRedirection();

// Map Pacientes endpoints
app.MapPacientesEndpoints();

app.Run();
