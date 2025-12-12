using Microsoft.EntityFrameworkCore;
using VollMed.Pacientes.Data;
using VollMed.Pacientes.Data.Repositories;
using VollMed.Pacientes.Domain.Interfaces;
using VollMed.Pacientes.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<VollMedDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

// Add Repositories
builder.Services.AddTransient<IPacienteRepository, PacienteRepository>();

// Add OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Map Pacientes endpoints
app.MapPacientesEndpoints();

app.Run();
