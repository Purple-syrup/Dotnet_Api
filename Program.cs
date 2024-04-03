using System.Net;
using Dotnet_Api.Data;
using Dotnet_Api.Extensions;
using Dotnet_Api.Helpers;
using Dotnet_Api.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<DataContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultPSQL"))
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

app.ConfigureExceptionHandler(app.Environment);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
