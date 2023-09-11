using Entities.Database;
using Logic.Manager;
using Logic.Manager.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSql"));
});
builder.Services.AddScoped<DbContext, ApplicationDbContext>();
builder.Services.AddScoped<IPartnerManager, PartnerManager>();
builder.Services.AddScoped<IRegistrationsManager, RegistrationsManager>();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigins", builder => builder.WithOrigins(
                            "http://localhost:4200"
                            )
                            .AllowAnyHeader()
                            .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request builder.Services.AddScoped<IRecaudosManager, RecaudosManager> ();pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
