using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using HorsesForCourses.Core.HorsesOnTheLoose;
using HorsesForCourses.WebApi;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using HorsesForCourses.Repo;

var builder = WebApplication.CreateBuilder(args);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HorsesForCourses API",
        Version = "v1",
        Description = "API voor het beheren van cursussen en coaches"
    });
});

//EF core
var padUitBin = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..")); //AppContext.BaseDirectory geeft map waar program wordt uitgevoerd
var dbPath = Path.Combine(padUitBin, "app.db"); //dynamisch pad zodat het niet in bin komt
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

//repo
builder.Services.AddScoped<ICoursesRepo, CoursesRepo>();
builder.Services.AddScoped<ICoachesRepo, CoachesRepo>();

//uow
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//controllers
builder.Services.AddControllers();

//algemene polly
builder.Services.AddResiliencePipeline("db-pipeline", builder =>
{
    builder
        .AddRetry(new RetryStrategyOptions())
        .AddTimeout(TimeSpan.FromSeconds(5));
});

var app = builder.Build();
// Console.WriteLine($"Database path: {dbPath}");

// migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseCors();

app.Use(async (context, next) =>
{
    try
    { await next(); } //waits between methods in controller
    catch (DomainException ex) //for creation of domainobjects
    {
        context.Response.StatusCode = 400;
        context.Response.ContentType = "application/problem+json";
        var problem = new ProblemDetails
        {
            Status = 400,
            Title = "Invalid Domain Arguments",
            Detail = ex.Message,
        };
        await context.Response.WriteAsJsonAsync(problem); //transforms problem into json
    }

    catch (NotReadyException ex) //for validation methods
    {
        context.Response.StatusCode = 409; //conflict in state of domainobjects
        context.Response.ContentType = "application/problem+json";
        var problem = new ProblemDetails
        {
            Status = 409,
            Title = "Requirements aren't met",
            Detail = ex.Message,
        };
        await context.Response.WriteAsJsonAsync(problem);
    }
});

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




