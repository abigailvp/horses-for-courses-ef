using HorsesForCourses.Core.HorsesOnTheLoose;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.WebApi.Controllers;
using HorsesForCourses.WebApi.Factory;
using HorsesForCourses.WebApi.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using NuGet.Frameworks;

namespace HorsesForCoursesTests;

public class CoachesControllerTests
{
    [Fact]
    public void Coach_Controller_Gets_All_Coaches()
    {
        AllData _myMemory = new AllData();
        CoachesController controller = new(_myMemory);
        var response = controller.GetCoaches();

        var okresult = Assert.IsType<OkObjectResult>(response.Result);
        var list = Assert.IsType<ListOfCoachesResponse>(okresult.Value);
        Assert.Empty(list.ListOfCoaches);
    }

    [Fact]
    public void Coach_Controller_Creates_Empty_Coach()
    {
        AllData _myMemory = new AllData();
        CoachesController controller = new(_myMemory);

        var dto = new CoachRequest
        {
            NameCoach = "Lola",
            Email = "l@example.com",
        };
        var result = controller.CreateEmptyCoach(dto);

        var okResult = Assert.IsType<OkObjectResult>(result.Result); //checkt en returnt type
        var coachid = Assert.IsType<int>(okResult.Value);
        Assert.Equal(0, coachid);

    }

    [Fact]
    public void Coach_Throws_Exception_When_Coach_Parameters_Are_Missing()
    {
        AllData _myMemory = new AllData();
        CoachesController controller = new(_myMemory);

        var dto = new CoachRequest
        {
            NameCoach = "",
            Email = "l@example.com",
        };

        var notWorking = Assert.Throws<DomainException>(() => controller.CreateEmptyCoach(dto));
        Assert.Equal("Name can't be empty", notWorking.Message);
    }

    [Fact]
    public void Coach_Controller_Gets_Coach_By_Id()
    {
        AllData _myMemory = new AllData();

        var coach = new Coach("Lola", "l@example.com");
        int coachId = coach.CoachId;

        _myMemory.allCoaches.Add(coach);
        CoachesController controller = new(_myMemory);

        var response = controller.GetCoachById(coachId);

        var detailedresult = Assert.IsType<OkObjectResult>(response.Result);
        var result = Assert.IsType<DetailedCoachResponse>(detailedresult.Value);
        Assert.Equal("Lola", result.Name);
        Assert.Empty(result.ListOfAssignedCourses);
        Assert.Empty(result.ListOfSkills);
    }

    [Fact]
    public void Coach_Controller_Doesnt_Get_NonExisting_Coach()
    {
        AllData _myMemory = new AllData();
        CoachesController controller = new(_myMemory);

        int coachId = 3;
        var response = controller.GetCoachById(coachId);

        Assert.IsType<NotFoundResult>(response.Result);
    }


}

