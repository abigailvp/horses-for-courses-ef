using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

// app.Use(async (context, next) =>
// {
//     try
//     { await next(); }
//     catch (Exception ex)
//     {
//         context.Response.StatusCode = 500;
//         var problem = new ProblemDetails
//         {
//             Status = 422,
//             Title = "My Error",
//             Detail = ex.Message,
//         };
//         await context.Response.WriteAsJsonAsync(problem);
//     }
// });

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


