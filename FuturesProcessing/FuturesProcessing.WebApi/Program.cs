using Common.BusinessLogic.DependencyInjection;
using Common.Data.ConnectionManager;
using Common.Data.Options;
using Common.Data.PostgreSql;
using Common.Data.Security;
using Common.WebApi.DependencyInjection;
using Common.WebApi.Setup;
using FuturesProcessing.Api.DataProviders;
using FuturesProcessing.BusinessLogic.DataProviders;
using FuturesProcessing.BusinessLogic.Job;
using Microsoft.Extensions.Options;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRegistriesAndProfiles();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConfiguredHttpClient(builder.Configuration.GetSection("FuturesProxy"))
    .AddTypedClient<IDataProvider, BinanceDataProvider>();

var connectionStringSection = builder.Configuration.GetSection("ConnectionStrings:PostgresSql");
if (connectionStringSection.Exists())
{
    builder.Services.Configure<ConnectionStringOptions>(connectionStringSection);
}
_ = builder.Services.AddSingleton<IConnectionManager, NpgConnectionManager>();
_ = builder.Services.AddTransient<IPostConfigureOptions<ConnectionStringOptions>, PostConfigureSecuredConnectionStringOptions>();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = new JobKey("DefaultArbitrageJob");
    q.AddJob<ArbitrageCalculationJob>(opts => opts
        .WithIdentity("DefaultArbitrageJob")
        .UsingJobData("firstFutureSymbol", "BTCUSDT_250627")
        .UsingJobData("secondFutureSymbol", "BTCUSDT_250926"));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("DefaultArbitrageTrigger")
        .WithCronSchedule("0 0/5 * ? * *"));
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

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

