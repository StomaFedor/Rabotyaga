using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html;
using AngleSharp.Html.Parser;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Umlaut;
using Umlaut.WebService.DBUpdaterService;
using Umlaut.WebService.DBUpdaterService.DBUpdaters;
using Umlaut.Database;
using Umlaut.Database.Repositories.FacultyRepository;
using Umlaut.Database.Repositories.GraduateRepository;
using Umlaut.Database.Repositories.LocationRepository;
using Umlaut.Database.Repositories.SpecializationRepository;
using MongoDB.Driver;
using Umlaut.Database.Repositories.MongoStatisticRepository;
using Umlaut.Database.Repositories.CommonStatisticRepository;
using Umlaut.Database.Repositories.LocationStatisticRepository;
using Umlaut.Database.Repositories.SpecializationStatisticRepository;
using Umlaut.Database.Repositories.FacultyStatisticRepository;
using MongoDB.Driver.Core.Configuration;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var connectionStringGraduateDB = builder.Configuration.GetConnectionString("PostgresGraduate");
var connectionStringStatisticsDB = builder.Configuration.GetConnectionString("StatisticsDb");
builder.Services.AddSingleton<IMongoDatabase>(new MongoClient(connectionStringStatisticsDB).GetDatabase("UmlautStat"));
builder.Services.AddDbContext<UmlautDBContext>(options => options.UseNpgsql(connectionStringGraduateDB));
builder.Services.AddTransient<ICommonStatisticRepository, CommonStatisticRepository>();
builder.Services.AddTransient<ILocationStatisticRepository, LocationStatisticRepository>();
builder.Services.AddTransient<ISpecializationStatisticRepository, SpecializationStatisticRepository>();
builder.Services.AddTransient<IFacultyStatisticRepository, FacultyStatisticRepository>();
builder.Services.AddTransient<HHruAPI>();
builder.Services.AddTransient<DBUpdateJob>();
builder.Services.AddTransient<IFacultyRepository, FacultyRepository>();
builder.Services.AddTransient<IGraduateRepository, GraduateRepository>();
builder.Services.AddTransient<ILocationRepository, LocationRepositopy>();
builder.Services.AddTransient<ISpecializationRepository, SpecializationRepositopy>();
builder.Services.AddTransient<GraduateDBUpdater>();
builder.Services.AddTransient<StatisticsDBUpdater>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    q.ScheduleJob<DBUpdateJob>(trigger => trigger
            .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(1)))
            .WithSimpleSchedule(x => x
                .WithIntervalInHours(10)
                .RepeatForever())
        );
});

builder.Services.AddQuartzHostedService(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
