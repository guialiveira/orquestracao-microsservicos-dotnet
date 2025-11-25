using VollMed.Web.Data;
using VollMed.Web.Filters;
using VollMed.Web.Interfaces;
using VollMed.Web.Repositories;
using VollMed.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ExceptionHandlerFilter>();

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ExceptionHandlerFilter>();
});

var connectionString = builder.Configuration.GetConnectionString("SqliteConnection");
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlite(connectionString));

var uri = new Uri(builder.Configuration["Medicos.ServiceAPI.Url"]!);
HttpClient httpClient = new HttpClient()
{
    BaseAddress = uri
};


builder.Services.AddHttpClient<IVollMedApiService, VollMedApiService>();

//builder.Services.AddTransient<IMedicoRepository, MedicoRepository>();
//builder.Services.AddTransient<IMedicoService, MedicoService>();

builder.Services.AddTransient<IConsultaRepository, ConsultaRepository>();
builder.Services.AddTransient<IConsultaService, ConsultaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/erro/500");
    app.UseStatusCodePagesWithReExecute("/erro/{0}");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
