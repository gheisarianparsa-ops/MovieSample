using Microsoft.EntityFrameworkCore;
using MovieWebApi.Data;
using MovieWebApi.Interfaces;
using MovieWebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<MovieAppDbContext>(q => q.UseSqlServer(builder.Configuration.GetConnectionString("MovieConnectionString")));
builder.Services.AddScoped<IMovieApp<MovieModel>, MovieRepository>();
builder.Services.AddScoped<IMovieApp<GenreModel>, GenreRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
