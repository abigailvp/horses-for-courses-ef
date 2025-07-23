


using HorsesForCourses.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICourseService, CourseService>();
builder.Services.AddSingleton<ICoachService, CoachService>();

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();