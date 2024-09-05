using Journal.Infrastructure.Extensions;
using JournalApi.Services;
using log4net;
using log4net.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IWeatherForecastService,WeatherForecastService>();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMemoryCache();
builder.Services.AddSingleton(LogManager.GetLogger(typeof(Program)));
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
