using Microsoft.Extensions.Configuration;
using SimulationDriver;
using webapi;
using webapi.Repositories;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ScadaDBContext>();
// Register repositories
builder.Services.AddScoped<IDigitalInputRepository, DigitalInputRepository>();
builder.Services.AddScoped<IDigitalOutputRepository, DigitalOutputRepository>();
builder.Services.AddScoped<IAnalogInputRepository, AnalogInputRepository>();
builder.Services.AddScoped<IAnalogOutputRepository, AnalogOutputRepository>();
builder.Services.AddScoped<IIOAddressRepository, IOAddressRepository>();
builder.Services.AddScoped<IRealTimeUnitRepository, RealTimeUnitRepository>();
builder.Services.AddScoped<ITagValueRepository, TagValueRepository>();
builder.Services.AddScoped<IAlarmRepository, AlarmRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAlarmTriggerRepository, AlarmTriggerRepository>();

// Register services
builder.Services.AddScoped<IDigitalInputService, DigitalInputService>();
builder.Services.AddScoped<IDigitalOutputService, DigitalOutputService>();
builder.Services.AddScoped<IAnalogInputService, AnalogInputService>();
builder.Services.AddScoped<IAnalogOutputService, AnalogOutputService>();
builder.Services.AddScoped<ITagValueService, TagValueService>();
builder.Services.AddScoped<IAlarmService, AlarmService>();
builder.Services.AddScoped<ITagProcessingService, TagProcessingService>();
builder.Services.AddScoped<IConfigurationFileService, ConfigurationFileService>();
builder.Services.AddScoped<IAlarmTriggerService, AlarmTriggerService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

using (var db = new ScadaDBContext())
{
    db.Database.EnsureCreated();
    db.SaveChanges();
}
using (var serviceProvider = builder.Services.BuildServiceProvider())
{
    var tagProcessingService = serviceProvider.GetService<ITagProcessingService>();
    // Call the method from TagValueService
    tagProcessingService.Process();
}
SimulationDriver.SimulationDriver simulationDriver = new SimulationDriver.SimulationDriver(new object());
simulationDriver.StartSimulation();
RealTimeDriver realTimeDriver = new RealTimeDriver(new object());
realTimeDriver.StartSimulation();
//TagProcessing tagProcessing = new TagProcessing(new object(), new object());
//tagProcessing.Process();
app.Run();