using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.HorsesOnTheLoose;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.WebApi.Controllers;
using HorsesForCourses.WebApi.Factory;
using HorsesForCourses.WebApi.Repo;
using Microsoft.AspNetCore.Mvc;

namespace HorsesForCoursesTests;

public class CoursesControllerTests : IClassFixture<CustomWebAppFactory>
{
    [Fact]
    public void Adds_Competences_To_The_Course()
    {
        AllData memory = new AllData();
        CoursesController controller = new CoursesController(memory);

        CompetentCourseRequest dto = new CompetentCourseRequest
        {
            CourseId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            ListOfCourseCompetences = new List<Skill>{
                new Competence( "Agility" , 5),
                new Competence( "Balance", 6 )
            }
        };

        Course course = new Course("HorseBackRiding", new DateOnly(2025, 6, 29), new DateOnly(2025, 7, 28));
        Guid idC = course.CourseId.value;
        memory.allCourses.Add(course);
        var result = controller.AddCompetences(idC, dto);

        OkObjectResult dtotje = Assert.IsType<OkObjectResult>(result.Result);
        var inhoud = Assert.IsType<CompetentCourseRequest>(dtotje.Value);
        Assert.Contains(new Competence("Agility", 5), inhoud.ListOfCourseCompetences);
    }

    [Fact]
    public void Doesnt_Add_Competences_To_Nonexisting_Course()
    {
        AllData memory = new AllData();
        CoursesController controller = new CoursesController(memory);

        CompetentCourseRequest dto = new CompetentCourseRequest
        {
            CourseId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            ListOfCourseCompetences = new List<Skill>{
                new Competence( "Agility" , 5),
                new Competence( "Balance", 6 )
            }
        };

        Guid idC = dto.CourseId;

        var result = controller.AddCompetences(idC, dto);

        Assert.IsType<NotFoundResult>(result.Result);

    }

    [Fact]
    public void Confirms_Course_If_It_Has_Timeslot_Longer_Than_An_Hour()
    {
        AllData memory = new AllData();
        CoursesController controller = new CoursesController(memory);

        ScheduledCourseRequest dto = new ScheduledCourseRequest
        {
            CourseId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            CourseTimeslots = new List<Timeslot>{
                new Timeslot( 9, 17, new DateOnly(2025, 6, 27)),
                new Timeslot( 9, 17, new DateOnly(2025, 7,18))
            }
        };

        Course course = new Course("HorseBackRiding", new DateOnly(2025, 6, 25), new DateOnly(2025, 7, 28));
        Guid idC = course.CourseId.value;
        memory.allCourses.Add(course);

        controller.AddTimeslots(idC, dto);
        var result = controller.ConfirmCourse(idC);

        OkObjectResult dtotje = Assert.IsType<OkObjectResult>(result.Result);
        var inhoud = Assert.IsType<CourseRequest>(dtotje.Value);
        Assert.Contains("HorseBackRiding", inhoud.NameCourse);

    }

    [Fact]
    public void Doesnt_Confirm_Course_If_It_Has_No_Timeslots()
    {
        AllData memory = new AllData();
        CoursesController controller = new CoursesController(memory);

        ScheduledCourseRequest dto = new ScheduledCourseRequest
        {
            CourseId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            CourseTimeslots = new List<Timeslot> { }
        };

        Course course = new Course("HorseBackRiding", new DateOnly(2025, 6, 25), new DateOnly(2025, 7, 28));
        Guid idC = course.CourseId.value;
        memory.allCourses.Add(course);

        controller.AddTimeslots(idC, dto);

        NotReadyException notWorking = Assert.Throws<NotReadyException>(() => controller.ConfirmCourse(idC));
        Assert.Contains("Course isn't ready yet", notWorking.Message);

    }

}