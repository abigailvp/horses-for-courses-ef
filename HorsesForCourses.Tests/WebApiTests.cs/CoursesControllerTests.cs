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
    // [Fact(Skip = "notready")]
    // public void Adds_Competences_To_The_Course()
    // {
    //     AllData memory = new AllData();
    //     CoursesController controller = new CoursesController(memory);

    //     CompetentCourseRequest dto = new CompetentCourseRequest
    //     {
    //         ListOfCourseCompetences = new List<Skill>{
    //             new Skill( "Agility"),
    //             new Skill( "Balance" )
    //         }
    //     };

    //     Course course = new Course("HorseBackRiding", new DateOnly(2025, 6, 29), new DateOnly(2025, 7, 28));
    //     int idC = course.CourseId;
    //     memory.allCourses.Add(course);
    //     var result = controller.AddCompetences(idC, dto);

    //     OkObjectResult dtotje = Assert.IsType<OkObjectResult>(result.Result);
    //     var inhoud = Assert.IsType<CompetentCourseRequest>(dtotje.Value);
    //     Assert.Contains(new Skill("Agility"), inhoud.ListOfCourseCompetences);
    // }

    // [Fact(Skip = "notready")]
    // public void Doesnt_Add_Competences_To_Nonexisting_Course()
    // {
    //     AllData memory = new AllData();
    //     CoursesController controller = new CoursesController(memory);

    //     CompetentCourseRequest dto = new CompetentCourseRequest
    //     {
    //         ListOfCourseCompetences = new List<Skill>{
    //             new Skill( "Agility"),
    //             new Skill( "Balance")
    //         }
    //     };

    //     int idC = dto.CourseId;

    //     var result = controller.AddCompetences(idC, dto);

    //     Assert.IsType<NotFoundResult>(result.Result);

    // }

    // [Fact(Skip = "notready")]
    // public void Confirms_Course_If_It_Has_Timeslot_Longer_Than_An_Hour()
    // {
    //     AllData memory = new AllData();
    //     CoursesController controller = new CoursesController(memory);

    //     ScheduledCourseRequest dto = new ScheduledCourseRequest
    //     {
    //         CourseTimeslots = new List<Timeslot>{
    //             new Timeslot( 9, 17, new DateOnly(2025, 6, 27)),
    //             new Timeslot( 9, 17, new DateOnly(2025, 7,18))
    //         }
    //     };

    //     Course course = new Course("HorseBackRiding", new DateOnly(2025, 6, 25), new DateOnly(2025, 7, 28));
    //     int idC = course.CourseId;
    //     memory.allCourses.Add(course);

    //     controller.AddTimeslots(idC, dto);
    //     var result = controller.ConfirmCourse(idC);

    //     OkObjectResult dtotje = Assert.IsType<OkObjectResult>(result.Result);
    //     var inhoud = Assert.IsType<CourseRequest>(dtotje.Value);
    //     Assert.Contains("HorseBackRiding", inhoud.NameCourse);

    // }

    // [Fact(Skip = "notready")]
    // public void Doesnt_Confirm_Course_If_It_Has_No_Timeslots()
    // {
    //     AllData memory = new AllData();
    //     CoursesController controller = new CoursesController(memory);

    //     ScheduledCourseRequest dto = new ScheduledCourseRequest
    //     {
    //         CourseTimeslots = new List<Timeslot> { }
    //     };

    //     Course course = new Course("HorseBackRiding", new DateOnly(2025, 6, 25), new DateOnly(2025, 7, 28));
    //     int idC = course.CourseId;
    //     memory.allCourses.Add(course);

    //     controller.AddTimeslots(idC, dto);

    //     NotReadyException notWorking = Assert.Throws<NotReadyException>(() => controller.ConfirmCourse(idC));
    //     Assert.Contains("Course isn't ready yet", notWorking.Message);

    // }

}