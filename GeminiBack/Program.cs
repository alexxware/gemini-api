using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using GeminiBack.Dtos;
using GeminiBack.Models;
using GeminiBack.Repository;
using GeminiBack.Service;
using GeminiBack.Validators;

var builder = WebApplication.CreateBuilder(args);

//configuracion de secretos
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(option =>
    {
        option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// inyeccion de dependencias Servicios
builder.Services.AddScoped<IGeminiService, GeminiService>();

// Repository
builder.Services.AddScoped<IGeminiRepository, GeminiRepository>();
builder.Services.AddScoped<IPromptStreamRepository, PromptStreamRepository>();

// API
builder.Services.Configure<ApiKeysOptions>(builder.Configuration.GetSection("ApiKeys"));

// no metodos extra configuration
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(option =>
{
    option.JsonSerializerOptions.UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow;
});

//Validators
builder.Services.AddScoped<IValidator<BasicPromptDto>, BasicPromptValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularDev");

app.UseAuthorization();

app.MapControllers();

app.Run();