using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.HorsesOnTheLoose;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.WebApi;
using HorsesForCourses.Repo;
using HorsesForCourses.WebApi.Controllers;
using HorsesForCourses.WebApi.Factory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace HorsesForCoursesTests;

public class CoursesControllerTests
{
    // [Fact]
    // public async Task Controller_Adds_Competences_To_The_Course()
    // {
    //     var connection = new SqliteConnection("Datasource=:memory:");
    //     await connection.OpenAsync();

    //     var options = new DbContextOptionsBuilder<AppDbContext>()
    //     .UseSqlite(connection)
    //     .Options;

    //     using (var context = new AppDbContext(options))
    //     {
    //         await context.Database.EnsureCreatedAsync();
    //     }

    //     using (var context = new AppDbContext(options))
    //     {
    //         var repo = new Repo(context);
    //         var trans = new UnitOfWork(context, repo);
    //         CoursesController controller = new CoursesController(trans);

    //         var coursedto = new CourseRequest { NameCourse = "HorseBackRiding", StartDateCourse = "2025/ 6/ 29", EndDateCourse = "2025/7/28" };
    //         var dto = new CompetentCourseRequest
    //         {
    //             ListOfCourseCompetences = new List<Skill> { new Skill("Agility"), new Skill("Balance") }
    //         };

    //         await controller.CreateEmptyCourse(coursedto);
    //         await controller.AddCompetences(1, dto);
    //     }

    //     using (var context = new AppDbContext(options))
    //     {
    //         var result = await context.Courses.FindAsync(1);
    //         Assert.Equal(1, result?.CourseId);
    //         Assert.Equal("HorseBackRiding", result?.NameCourse);
    //         Assert.Equal(2, result?.ListOfCourseSkills.Count());
    //         Assert.IsType<Skill>(result?.ListOfCourseSkills[0]);
    //         Assert.Equal("Agility", result?.ListOfCourseSkills[0].Name);
    //     }

    //     await connection.CloseAsync();

    // }

    // [Fact]
    // public async Task Doesnt_Add_Competences_To_Nonexisting_Course()
    // {
    //     var connection = new SqliteConnection("Datasource=:memory:");
    //     await connection.OpenAsync();

    //     var options = new DbContextOptionsBuilder<AppDbContext>()
    //     .UseSqlite(connection)
    //     .Options;

    //     using (var ctx = new AppDbContext(options))
    //     {
    //         await ctx.Database.EnsureCreatedAsync();
    //     }
    //     using (var ctx = new AppDbContext(options))
    //     {
    //         var repo = new Repo(ctx);
    //         var trans = new UnitOfWork(ctx, repo);
    //         CoursesController controller = new(trans);
    //         CompetentCourseRequest dto = new CompetentCourseRequest
    //         {
    //             ListOfCourseCompetences = new List<Skill> { new Skill("Agility"), new Skill("Balance") }
    //         };

    //         var result = controller.AddCompetences(10, dto);
    //         Assert.IsType<NotFoundResult>(result?.Result);
    //     }

    //     await connection.CloseAsync();
    // }

    // [Fact]
    // public async Task Confirms_Course_If_It_Has_Timeslot_Longer_Than_An_Hour()
    // {
    //     var connection = new SqliteConnection("Datasource=:memory:");
    //     await connection.OpenAsync();

    //     var options = new DbContextOptionsBuilder<AppDbContext>()
    //     .UseSqlite(connection)
    //     .Options;

    //     using (var ctx = new AppDbContext(options))
    //     {
    //         await ctx.Database.EnsureCreatedAsync();
    //     }

    //     using (var ctx = new AppDbContext(options))
    //     {
    //         var repo = new Repo(ctx);
    //         var trans = new UnitOfWork(ctx, repo);
    //         CoursesController controller = new(trans);
    //         ScheduledCourseRequest dtolist = new ScheduledCourseRequest
    //         {
    //             CourseTimeslots = new List<MyTimeslot>{
    //                     new (  "2025, 6, 27", 9, 17),
    //                     new (  "2025, 7,18", 9, 17)
    //                 }
    //         };
    //         var coursedto = new CourseRequest { NameCourse = "HorseBackRiding", StartDateCourse = "2025/ 6/ 29", EndDateCourse = "2025/7/28" };
    //         await controller.CreateEmptyCourse(coursedto);
    //         await controller.AddTimeslots(1, dtolist);
    //         var result = await controller.ConfirmCourse(1);

    //         Assert.IsType<OkResult>(result);
    //     }
    //     await connection.CloseAsync();
    // }

    // [Fact]
    // public async Task Doesnt_Confirm_Course_If_It_Has_No_Timeslots()
    // {
    //     var connection = new SqliteConnection("Datasource=:memory:");
    //     await connection.OpenAsync();

    //     var options = new DbContextOptionsBuilder<AppDbContext>()
    //     .UseSqlite(connection)
    //     .Options;

    //     using (var ctx = new AppDbContext(options))
    //     {
    //         await ctx.Database.EnsureCreatedAsync();
    //     }

    //     using (var ctx = new AppDbContext(options))
    //     {
    //         var repo = new Repo(ctx);
    //         var trans = new UnitOfWork(ctx, repo);
    //         CoursesController controller = new CoursesController(trans);

    //         ScheduledCourseRequest dto = new ScheduledCourseRequest
    //         {
    //             CourseTimeslots = new List<MyTimeslot> { }
    //         };

    //         var coursedto = new CourseRequest { NameCourse = "Chess", StartDateCourse = "2025/ 6/ 29", EndDateCourse = "2025/7/28" };
    //         await controller.CreateEmptyCourse(coursedto);
    //         await controller.AddTimeslots(1, dto);

    //         var notWorking = Assert.ThrowsAsync<NotReadyException>(async () => await controller.ConfirmCourse(1));
    //         Assert.Contains("Course doesn't have timeslots", notWorking.Result.Message);
    //     }
    //     await connection.CloseAsync();
    // }

    // [Fact]
    // public async Task Assigns_Course_If_Coach_Available_And_Competent()
    // {
    //     var connection = new SqliteConnection("Datasource=:memory:");
    //     await connection.OpenAsync();

    //     var options = new DbContextOptionsBuilder<AppDbContext>()
    //     .UseSqlite(connection)
    //     .Options;

    //     using (var ctx = new AppDbContext(options))
    //     {
    //         await ctx.Database.EnsureCreatedAsync();
    //     }

    //     using (var ctx = new AppDbContext(options))
    //     {
    //         var repo = new Repo(ctx);
    //         var trans = new UnitOfWork(ctx, repo);
    //         CoursesController controller = new(trans);

    //         Coach coach = new("Abi", "abi@mail.com");
    //         var Timeslots = new List<Timeslot>
    //             {
    //                 new ( 9, 17, new DateOnly(2025, 6, 27) ),
    //                 new ( 9, 17, new DateOnly(2025, 7, 18) )
    //             };
    //         var Competences = new List<Skill> { new Skill("Agility"), new Skill("Balance") };
    //         Course course = new("HorseBackRiding", new DateOnly(2025, 6, 29), new DateOnly(2025, 7, 28));
    //         coach.AddCompetenceList(Competences);
    //         course.AddCompetenceList(Competences);
    //         course.AddTimeSlotList(Timeslots);
    //         ctx.Coaches.Add(coach);
    //         ctx.Courses.Add(course);
    //         await ctx.SaveChangesAsync();

    //         var dto = new AssignedCourseRequest { coachId = 1 };

    //         var result = await controller.AssignCoach(1, dto);
    //         Assert.IsType<OkResult>(result);
    //         Assert.True(course.hasCoach);
    //         Assert.Equal(1, coach.numberOfAssignedCourses);
    //         Assert.Contains(course, coach.ListOfCoursesAssignedTo);
    //     }
    //     await connection.CloseAsync();
    // }

    // [Fact]
    // public async Task Doesnt_Assign_Course_If_Coach_Not_Competent()
    // {
    //     var connection = new SqliteConnection("Datasource=:memory:");
    //     await connection.OpenAsync();

    //     var options = new DbContextOptionsBuilder<AppDbContext>()
    //     .UseSqlite(connection)
    //     .Options;

    //     using (var ctx = new AppDbContext(options))
    //     {
    //         await ctx.Database.EnsureCreatedAsync();
    //     }

    //     using (var ctx = new AppDbContext(options))
    //     {
    //         var repo = new Repo(ctx);
    //         var trans = new UnitOfWork(ctx, repo);

    //         CoachesController controller = new(trans);
    //         var otherskills = new CompetentCoachRequest { ListOfSkills = new List<Skill> { new Skill("sleeping") } };
    //         await controller.CreateEmptyCoach(new CoachRequest { NameCoach = "abi", Email = "abi@mail.com" });
    //         await controller.AddCompetencesList(1, otherskills);
    //     }

    //     using (var ctx = new AppDbContext(options))
    //     {
    //         var repo = new Repo(ctx);
    //         var trans = new UnitOfWork(ctx, repo);
    //         CoursesController controller = new(trans);

    //         var coursedto = new CourseRequest { NameCourse = "HorseBackRiding", StartDateCourse = "2025/ 6/ 29", EndDateCourse = "2025/7/28" };
    //         var dto = new CompetentCourseRequest
    //         { ListOfCourseCompetences = new List<Skill> { new("Agility"), new("Balance") } };
    //         ScheduledCourseRequest dtolist = new ScheduledCourseRequest
    //         { CourseTimeslots = new List<MyTimeslot> { new("2025, 6, 27", 9, 17), new("2025, 7,18", 9, 17) } };
    //         var assigndto = new AssignedCourseRequest { coachId = 1 };

    //         await controller.CreateEmptyCourse(coursedto);
    //         await controller.AddCompetences(1, dto);
    //         await controller.AddTimeslots(1, dtolist);
    //         await controller.ConfirmCourse(1);

    //         var allcoursesresult = Assert.ThrowsAsync<NotReadyException>(async () => await controller.AssignCoach(1, assigndto));
    //         Assert.Equal("Coach doesn't have necessary skills", allcoursesresult.Result.Message);

    //     }
    //     await connection.CloseAsync();
    // }

    // [Fact]
    // public async Task Doesnt_Assign_Course_If_Coach_No_timeslots()
    // {
    //     var connection = new SqliteConnection("Datasource=:memory:");
    //     await connection.OpenAsync();

    //     var options = new DbContextOptionsBuilder<AppDbContext>()
    //     .UseSqlite(connection)
    //     .Options;

    //     using (var ctx = new AppDbContext(options))
    //     {
    //         await ctx.Database.EnsureCreatedAsync();
    //     }
    //     using (var ctx = new AppDbContext(options))
    //     {
    //         var repo = new Repo(ctx);
    //         var trans = new UnitOfWork(ctx, repo);

    //         CoachesController controller = new(trans);
    //         var dtolist = new CompetentCoachRequest
    //         { ListOfSkills = new List<Skill> { new Skill("Agility"), new Skill("Balance") } };
    //         await controller.CreateEmptyCoach(new CoachRequest { NameCoach = "abi", Email = "abi@mail.com" });
    //         await controller.AddCompetencesList(1, dtolist);
    //     }

    //     using (var ctx = new AppDbContext(options))
    //     {
    //         var repo = new Repo(ctx);
    //         var trans = new UnitOfWork(ctx, repo);
    //         CoursesController controller = new(trans);

    //         var coursedto = new CourseRequest { NameCourse = "HorseBackRiding", StartDateCourse = "2025/ 6/ 29", EndDateCourse = "2025/7/28" };
    //         var dto = new CompetentCourseRequest
    //         { ListOfCourseCompetences = new List<Skill> { new Skill("Agility"), new Skill("Balance") } };
    //         ScheduledCourseRequest dtolist = new ScheduledCourseRequest
    //         { CourseTimeslots = new List<MyTimeslot> { new("2025, 6, 27", 9, 17), new("2025, 7,18", 9, 17) } };
    //         var assigndto = new AssignedCourseRequest { coachId = 1 };

    //         await controller.CreateEmptyCourse(coursedto);
    //         await controller.AddCompetences(1, dto);
    //         await controller.AddTimeslots(1, dtolist);
    //         await controller.ConfirmCourse(1);
    //         await controller.AssignCoach(1, assigndto);

    //         var allcoursesresult = Assert.ThrowsAsync<NotReadyException>(async () => await controller.AssignCoach(1, assigndto));
    //         Assert.Equal("Coach is not available", allcoursesresult.Result.Message);

    //     }
    //     await connection.CloseAsync();
    // }


    // [Fact]
    // public async Task Gets_Assigned_Courses()
    // {
    //     var connection = new SqliteConnection("Datasource=:memory:");
    //     await connection.OpenAsync();

    //     var options = new DbContextOptionsBuilder<AppDbContext>()
    //     .UseSqlite(connection)
    //     .Options;

    //     using (var ctx = new AppDbContext(options))
    //     {
    //         await ctx.Database.EnsureCreatedAsync();
    //     }
    //     using (var ctx = new AppDbContext(options))
    //     {
    //         Coach coach = new("Abi", "abi@mail.com");
    //         var Timeslots = new List<Timeslot>
    //             {
    //                 new ( 9, 17, new DateOnly(2025, 6, 27) ),
    //                 new ( 9, 17, new DateOnly(2025, 7, 18) )
    //             };
    //         var Competences = new List<Skill> { new Skill("Agility"), new Skill("Balance") };
    //         Course course = new("HorseBackRiding", new DateOnly(2025, 6, 29), new DateOnly(2025, 7, 28));
    //         coach.AddCompetenceList(Competences);
    //         course.AddCompetenceList(Competences);
    //         course.AddTimeSlotList(Timeslots);
    //         course.AddingCoach(course, coach);
    //         ctx.Coaches.Add(coach);
    //         ctx.Courses.Add(course);
    //         await ctx.SaveChangesAsync();
    //     }
    //     using (var ctx = new AppDbContext(options))
    //     {
    //         var repo = new Repo(ctx);
    //         var trans = new UnitOfWork(ctx, repo);
    //         CoursesController controller = new(trans);
    //         var result = await controller.GetCourses();

    //         var coursesresult = Assert.IsType<OkObjectResult>(result.Result);
    //         var list = Assert.IsType<AllCoursesResponse>(coursesresult.Value);
    //         Assert.NotEmpty(list.ListOfCourses);
    //         Assert.Single(list.ListOfCourses);
    //         Assert.Equal("HorseBackRiding", list.ListOfCourses[0].name);
    //         Assert.True(list.ListOfCourses[0].hasCoach);
    //         Assert.True(list.ListOfCourses[0].hasSchedule);

    //     }


    //     await connection.CloseAsync();
    // }


}