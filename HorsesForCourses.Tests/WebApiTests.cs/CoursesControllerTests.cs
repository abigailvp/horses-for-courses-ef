using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.HorsesOnTheLoose;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.WebApi;
using HorsesForCourses.WebApi.Controllers;
using HorsesForCourses.WebApi.Factory;
using Microsoft.AspNetCore.Mvc;

namespace HorsesForCoursesTests;

// public class CoursesControllerTests
// {
//     [Fact]
//     public void Controller_Adds_Competences_To_The_Course()
//     {
//         AllData memory = new AllData();
//         CoursesController controller = new CoursesController(memory);

//         CompetentCourseRequest dto = new CompetentCourseRequest
//         {
//             ListOfCourseCompetences = new List<Skill>{
//                 new Skill( "Agility"),
//                 new Skill( "Balance" )
//             }
//         };

//         Course course = new Course("HorseBackRiding", new DateOnly(2025, 6, 29), new DateOnly(2025, 7, 28));
//         int id = course.CourseId;
//         memory.allCourses.Add(course);

//         var result = controller.AddCompetences(id, dto);

//         Assert.IsType<OkResult>(result);

//     }

//     [Fact]
//     public void Doesnt_Add_Competences_To_Nonexisting_Course()
//     {
//         AllData memory = new AllData();
//         CoursesController controller = new CoursesController(memory);

//         CompetentCourseRequest dto = new CompetentCourseRequest
//         {
//             ListOfCourseCompetences = new List<Skill>{
//                 new Skill( "Agility"),
//                 new Skill( "Balance")
//             }
//         };

//         var result = controller.AddCompetences(0, dto);

//         Assert.IsType<NotFoundResult>(result);
//     }

//     [Fact]
//     public void Confirms_Course_If_It_Has_Timeslot_Longer_Than_An_Hour()
//     {
//         AllData memory = new AllData();
//         CoursesController controller = new CoursesController(memory);

//         ScheduledCourseRequest dto = new ScheduledCourseRequest
//         {
//             CourseTimeslots = new List<MyTimeslot>{
//                 new (  "2025, 6, 27", 9, 17),
//                 new (  "2025, 7,18", 9, 17)
//             }
//         };

//         Course course = new Course("HorseBackRiding", new DateOnly(2025, 6, 25), new DateOnly(2025, 7, 28));
//         int idC = course.CourseId;
//         memory.allCourses.Add(course);

//         controller.AddTimeslots(idC, dto);
//         var result = controller.ConfirmCourse(idC);

//         Assert.IsType<OkResult>(result);


//     }

//     [Fact]
//     public void Doesnt_Confirm_Course_If_It_Has_No_Timeslots()
//     {
//         AllData memory = new AllData();
//         CoursesController controller = new CoursesController(memory);

//         ScheduledCourseRequest dto = new ScheduledCourseRequest
//         {
//             CourseTimeslots = new List<MyTimeslot> { }
//         };

//         Course course = new Course("HorseBackRiding", new DateOnly(2025, 6, 25), new DateOnly(2025, 7, 28));
//         int idC = course.CourseId;
//         memory.allCourses.Add(course);

//         controller.AddTimeslots(idC, dto);

//         NotReadyException notWorking = Assert.Throws<NotReadyException>(() => controller.ConfirmCourse(idC));
//         Assert.Contains("Course doesn't have timeslots", notWorking.Message);

//     }


// }