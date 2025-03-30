using Common.BusinessLogic.DependencyInjection;
using Common.WebApi.DependencyInjection;
using Common.WebApi.Setup;
using FuturesProcessing.Api.DataProviders;
using FuturesProcessing.BusinessLogic.DataProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRegistriesAndProfiles();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConfiguredHttpClient(builder.Configuration.GetSection("FuturesProxy"))
    .AddTypedClient<IDataProvider, BinanceDataProvider>();

builder.Services.ConfigureLogging(builder.Configuration);

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

