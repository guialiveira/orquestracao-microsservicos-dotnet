using Microsoft.EntityFrameworkCore;
using VollMed.Consultas.Data;
using VollMed.Consultas.Data.Repositories;
using VollMed.Consultas.Domain.Interfaces;
using VollMed.Consultas.Endpoints;
using Refit;
using VollMed.Consultas.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<VollMedDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

// Add Repositories
builder.Services.AddTransient<IConsultaRepository, ConsultaRepository>();

// Add OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddRefitClient<IMedicosApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7001"));

builder.Services
    .AddRefitClient<IPacientesApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7002"));

var app = builder.Build();

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
app.MapConsultasEndpoints();

app.Run();
