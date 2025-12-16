using MassTransit;
using Microsoft.EntityFrameworkCore;
using Refit;
using VollMed.Consultas.Data;
using VollMed.Consultas.Data.Repositories;
using VollMed.Consultas.Domain.Interfaces;
using VollMed.Consultas.Endpoints;
using VollMed.Consultas.Handlers;
using VollMed.Consultas.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<AuthHandler>();

// Add DbContext
builder.Services.AddDbContext<VollMedDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

// Add Repositories
builder.Services.AddTransient<IConsultaRepository, ConsultaRepository>();

// Add OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string apiUris = builder.Configuration["Api:Uri"]
    ?? throw new Exception("Uri da API VollMed não configurada!");

builder.Services
    .AddRefitClient<IMedicosApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiUris))
    .AddHttpMessageHandler<AuthHandler>();

builder.Services
    .AddRefitClient<IPacientesApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiUris))
    .AddHttpMessageHandler<AuthHandler>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        var rabbitMqConfig = builder.Configuration.GetSection("RabbitMQ");

        cfg.Host(rabbitMqConfig["Host"], Convert.ToUInt16(rabbitMqConfig["Port"]), "/", h =>
        {
            h.Username(rabbitMqConfig["Username"]!);
            h.Password(rabbitMqConfig["Password"]!);
        });
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

// Map Consultas endpoints
app
    .MapConsultasEndpoints()
    .MapReceitasEndpoints();

app.Run();
