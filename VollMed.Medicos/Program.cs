using Microsoft.EntityFrameworkCore;
using VollMed.Medicos.Data;
using VollMed.Medicos.Data.Repositories;
using VollMed.Medicos.Domain.Interfaces;
using VollMed.Medicos.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<VollMedDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

// Add Repositories
builder.Services.AddTransient<IMedicoRepository, MedicoRepository>();

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

app.UseHttpsRedirection();

// Map Medicos endpoints
app.MapMedicosEndpoints();

app.Run();
