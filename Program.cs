using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using EmpacadoraLimonAPI.Models;
using EmpacadoraLimonAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DbEmpacadoraContext>(
    opciones => opciones.UseNpgsql("name=ConnectionStrings:postgresConnection")
);

builder.Services.AddControllers().AddJsonOptions(
    opciones =>
    {
        opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    }
);

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAlmacenamiento, Almacenamiento>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(
   opciones =>
{
    opciones.AddPolicy(name: "Default",
        policy =>
        {
            policy.WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("Default");

app.UseAuthorization();

app.MapControllers();

app.Run();
