using Medicos.ServiceAPI.Data;
using Medicos.ServiceAPI.Interfaces;
using Medicos.ServiceAPI.Repositories;
using Medicos.ServiceAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<MedicosDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")
));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IMedicoRepository, MedicoRepository>();
builder.Services.AddTransient<IMedicoService, MedicoService>();
builder.Services.AddTransient<IConsultaRepository, ConsultaRepository>();
builder.Services.AddTransient<IConsultaService, ConsultaService>();
builder.Services.AddTransient<IPacienteRepository, PacienteRepository>();
builder.Services.AddTransient<IPacienteService, PacienteService>();

var app = builder.Build();

// Seed the database with initial data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MedicosDbContext>();
    DbSeeder.Seed(context);
}

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
