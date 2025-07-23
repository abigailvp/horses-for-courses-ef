using System.Reflection.Metadata.Ecma335;
using HorsesForCourses.Core;
using CoachControllers;
using HorsesForCourses.Services;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCoursesTests;

public class CoachControllerTest
{
    [Fact]
    public void Coach_Controller_Gets()
    {
        Availability available = new();
        Adding adder = new();
        CoachService server = new(adder, available);
        CoachController controller = new(server);
        var response = controller.GetAllCoaches();
        Assert.Equal(response, AllData.allCoaches);

    }

    [Fact(Skip = "not ready")]
    public void Coach_Controller_Posts()
    {
        Availability available = new();
        Adding adder = new();
        CoachService server = new(adder, available);
        CoachController controller = new(server);

        var dto = new CoachDTO
        {
            CoachId = Guid.NewGuid(),
            NameCoach = "Lola",
            Email = "l@example.com",
            ListOfCompetences = new List<Competence>
            {
                new ("Communication", 3 )
            },

            AvailableTimeslots = new Dictionary<DateOnly, List<Timeslot>>
            {
                [new DateOnly(2025, 7, 24)] = new List<Timeslot>
                {
                    new Timeslot(9,11, new DateOnly(2025,7,23))
                }
            }
        };

        var response = controller.CreateCoach(Guid.NewGuid(), dto);

        Assert.Equal(response, "Coach Lola was added.");
    }
}