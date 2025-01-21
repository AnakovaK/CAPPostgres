using CAPPostgres;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Savorboard.CAP.InMemoryMessageQueue;

var builder = WebApplication.CreateBuilder(args);

var lazyDataSource = new Lazy<NpgsqlDataSource>(() =>
{
    var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("PostgresConnection"));
    return dataSourceBuilder.Build();
});
builder.Services.AddDbContextFactory<CapDbContext>(config => config
    .UseNpgsql(lazyDataSource.Value, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));

builder.Services.AddCap(options =>
{
    //Breaks everything as we are not using connection string
    options.UseEntityFramework<CapDbContext>();

    // Will work as we pass connection string straight on
    //options.UsePostgreSql(builder.Configuration.GetConnectionString("PostgresConnection"));

    options.UseInMemoryMessageQueue();
});

var app = builder.Build();

app.Run();
