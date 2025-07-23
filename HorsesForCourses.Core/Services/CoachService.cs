using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.Core;

namespace HorsesForCourses.Services;

public class CoachService : ICoachService
{
    private readonly IAdding _adding;
    private readonly IAvailability _availability;

    public CoachService(Adding adder, Availability availability)
    {
        _adding = adder;
        _availability = availability;
    }

    public string CreateAndAssignCoach(Course course, CoachDTO dto)
    {
        var coach = _adding.createCoach(dto);
        var status = _availability.CheckCoachAvailability(course, coach);
        // _availability.

        if (status != StatusCourse.Assigned)
            return "Coach isn't available or competent for course";
        AllData.assignedCoaches.Add(coach);
        return $"Coach {coach.NameCoach} was added.";
    }
}