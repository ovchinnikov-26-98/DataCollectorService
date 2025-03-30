using Common.Api.Operations;
using Common.BusinessLogic.DependencyInjection;
using Common.Data.ConnectionManager;
using Common.Data.Options;
using Common.Data.PostgreSql;
using Common.Data.Security;
using Common.WebApi.DependencyInjection;
using Common.WebApi.Setup;
using DataCollector.Api.Contract;
using DataCollector.Api.Item;
using DataCollector.BusinessLogic.Operations;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRegistriesAndProfiles();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConfiguredHttpClient(builder.Configuration.GetSection("BinanceProxy"))
    .AddTypedClient<IOperationAsync<FuturesContract, FuturesPriceItem>, GetFuturesPricesOperationAsync>();

var connectionStringSection = builder.Configuration.GetSection("ConnectionStrings:PostgresSql");
if (connectionStringSection.Exists())
{
    builder.Services.Configure<ConnectionStringOptions>(connectionStringSection);
}
_ = builder.Services.AddSingleton<IConnectionManager, NpgConnectionManager>();
_ = builder.Services.AddTransient<IPostConfigureOptions<ConnectionStringOptions>, PostConfigureSecuredConnectionStringOptions>();


builder.Services.ConfigureLogging(builder.Configuration);

//_ = builder.Host.UseNlog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseConfiguredLogging();
//app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
