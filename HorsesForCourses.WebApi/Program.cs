using HorsesForCourses.WebApi.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using HorsesForCourses.Core.HorsesOnTheLoose;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<AllData>();
builder.Services.AddControllers();
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

var app = builder.Build();

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


