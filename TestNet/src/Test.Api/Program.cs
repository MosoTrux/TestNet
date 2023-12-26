using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Reflection;
using TestNet.Application.Services.Implements;
using TestNet.Application.Services.Interfaces;
using TestNet.Core.Repositories;
using TestNet.Infrastructure.Repositories;
using Serilog;
using TestNet.Api.Middlewares;
using TestNet.Core.Repositories.Mockapi.io;
using TestNet.Infrastructure.Repositories.Mockapi.io;
using TestNet.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Log/application.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSerilog();
});
// Add services to the container.


builder.Services.AddControllers();

var connString = configuration.GetConnectionString("TestNet");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.Configure<AppSettings>(
    builder.Configuration
        .GetSection("AppSettings")
);

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(DapperRepository<>));
builder.Services.AddTransient<IDapperUnitOfWork, DapperUnitOfWork>();
builder.Services.AddTransient<IMockapiIORespository, MockapiIORespository>();

builder.Services.AddSingleton<IProductService, ProductService>();

var app = builder.Build();
app.UseMiddleware<PerfomanceLoggingMiddleware>();

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
Log.CloseAndFlush();

