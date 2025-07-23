


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICourseDTO, CourseDTO>();
builder.Services.AddSingleton<ICoachDTO, CoachDTO>();

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();