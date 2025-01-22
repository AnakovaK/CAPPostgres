using CAPPostgres;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Savorboard.CAP.InMemoryMessageQueue;

var builder = WebApplication.CreateBuilder(args);


var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("PostgresConnection"));

builder.Services.AddDbContextFactory<CapDbContext>(config => config
    .UseNpgsql(dataSourceBuilder.Build()));

builder.Services.AddCap(options =>
{
    //Breaks everything as we are not using connection string without "Persist Security Info = true")
    options.UseEntityFramework<CapDbContext>();

    // Will work as we pass connection string straight on
    //options.UsePostgreSql(builder.Configuration.GetConnectionString("PostgresConnection"));

    options.UseInMemoryMessageQueue();
});

var app = builder.Build();


await app.RunAsync(new CancellationTokenSource(TimeSpan.FromSeconds(30)).Token);
