using FluentValidation;
using Microsoft.OpenApi.Models;
using Zapchat.Domain.Interfaces;
using Zapchat.Repository.Data;
using Zapchat.Repository.Repositories;
using Zapchat.Domain.DTOs;
using Zapchat.Service.Mappings;
using Zapchat.Service.Validations;
using Microsoft.EntityFrameworkCore;
using Zapchat.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext com SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de dependência
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IGrupoWhatsAppRepository, GrupoWhatsAppRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IGrupoWhatsAppService, GrupoWhatsAppService>();
builder.Services.AddScoped<IValidator<UsuarioDto>, UsuarioValidator>();



// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(UsuarioProfile));

// Adicionando Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Zapchat", Version = "v1" });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

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
