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
    { await next(); } //wacht tussen methodes van controller
    catch (DomainException ex) //bij maken van domeinobjecten
    {
        context.Response.StatusCode = 400;
        context.Response.ContentType = "application/problem+json";
        var problem = new ProblemDetails
        {
            Status = 400,
            Title = "Invalid Domain Arguments",
            Detail = ex.Message,
        };
        await context.Response.WriteAsJsonAsync(problem); //zet problem om in json
    }

    // catch (NotReadyException ex) //bij maken van domeinobjecten
    // {
    //     context.Response.StatusCode = 400;
    //     context.Response.ContentType = "application/problem+json";
    //     var problem = new ProblemDetails
    //     {
    //         Status = 400,
    //         Title = "Invalid Domain Arguments",
    //         Detail = ex.Message,
    //     };
    //     await context.Response.WriteAsJsonAsync(problem); //zet problem om in json
    // }
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


