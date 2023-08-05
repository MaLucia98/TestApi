using Entities.Database;
using Logic.Manager;
using Logic.Manager.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

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
builder.Services.AddScoped<IDataApiManager, DataApiManager>();
builder.Services.AddScoped<IRecaudosManager, RecaudosManager>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsImplementationPolicy", builder => builder.WithOrigins("*"));
});

var app = builder.Build();

// Configure the HTTP request builder.Services.AddScoped<IRecaudosManager, RecaudosManager> ();pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCorsImplementationPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
