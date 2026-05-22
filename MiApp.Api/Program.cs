using MiApp.Application;
using Microsoft.EntityFrameworkCore;
using MiApp.Domain.Interfaces;
using MiApp.Infrastructure.Persistence;
using MiApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. REGISTRAR EL ADDDBCONTEXT PARA SQLITE
builder.Services.AddApplication();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. REGISTRAR LOS REPOSITORIOS Y UNIT OF WORK (CICLO DE VIDA SCOPED)
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Soporte para controladores clásicos
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();