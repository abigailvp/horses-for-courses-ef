using HorsesForCourses.Core.HorsesOnTheLoose;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.WebApi.Controllers;
using HorsesForCourses.WebApi.Factory;
using HorsesForCourses.WebApi.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HorsesForCoursesTests;

public class CoachesControllerTests
{
    [Fact]
    public void Coach_Controller_Gets_All_Coaches()
    {
        AllData _myMemory = new AllData();
        CoachesController controller = new(_myMemory);
        var response = controller.GetCoaches();

        Assert.Equal(response, _myMemory.allCoaches);
    }

    [Fact]
    public void Coach_Controller_Creates_Empty_Coach()
    {
        AllData _myMemory = new AllData();
        CoachesController controller = new(_myMemory);

        var dto = new CoachRequest
        {
            CoachId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            NameCoach = "Lola",
            Email = "l@example.com",
        };
        var result = controller.CreateEmptyCoach(dto);

        var okResult = Assert.IsType<OkObjectResult>(result.Result); //checkt en returnt type
        var message = Assert.IsType<string>(okResult.Value);
        Assert.Contains("Lola", message);
    }

    [Fact]
    public void Coach_Throws_Exception_When_Coach_Parameters_Are_Missing()
    {
        AllData _myMemory = new AllData();
        CoachesController controller = new(_myMemory);

        var dto = new CoachRequest
        {
            CoachId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            NameCoach = "",
            Email = "l@example.com",
        };

        Assert.Throws<DomainException>(() => controller.CreateEmptyCoach(dto));
    }

    [Fact]
    public void Coach_Controller_Gets_Coach_By_Id()
    {
        AllData _myMemory = new AllData();

        var coach = new Coach("Lola", "l@example.com");

        _myMemory.allCoaches.Add(coach);
        CoachesController controller = new(_myMemory);

        Guid coachId = Guid.NewGuid();
        var response = controller.GetCoachById(coachId);

        Assert.IsType<NotFoundResult>(response.Result);
    }

    // [Fact]
    // public void Coach_Adds_Timeslots()
    // {
    //     AllData _myMemory = new AllData();
    //     CoachesController controller = new(_myMemory);

    //     var coach = new Coach("Lola", "l@example.com");
    //     _myMemory.allCoaches.Add(coach);

    //     Guid idWow = new Guid();
    //     List<Timeslot> list = new List<Timeslot>
    //             {
    //                 new Timeslot(9,11, new DateOnly(2025,7,23))
    //             };
    //     ScheduledCoachRequest dto = new ScheduledCoachRequest
    //     {
    //         CoachId = idWow,
    //         CoachTimeslots = list
    //     };

    //     var response = controller.AddTimeslots(idWow, dto);
    //     Assert.IsType<OkObjectResult>(response.Result);
    // }
}




//  var dto = new CoachRequest
//  {
//      CoachId = Guid.NewGuid(),
//      NameCoach = "Lola",
//      Email = "l@example.com",
//      ListOfCompetences = new List<Competence>
//             {
//                 new ("Communication", 3 )
//             },

//      AvailableTimeslots = new Dictionary<DateOnly, List<Timeslot>>
//      {
//          [new DateOnly(2025, 7, 24)] = new List<Timeslot>
//                 {
//                     new Timeslot(9,11, new DateOnly(2025,7,23))
//                 }
//      }
//  };