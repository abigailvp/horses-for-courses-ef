using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.Core;

namespace HorsesForCourses.Services;

public class CourseService : ICourseService
{
    private readonly IAdding _adder;
    private readonly IAvailability _availability;

    public CourseService(Adding adder, Availability availability)
    {
        _adder = adder;
        _availability = availability;
    }

    public string CreateEmptyCourse(CourseDTO dto)
    {
        var course = _adder.createCourse(dto);
        return $"Course has the name {course.NameCourse}. It starts at {course.EndDateCourse} and ends at {course.EndDateCourse}.";
    }

    public string CreateAndAssignCourse(Coach coach, CourseDTO dto)
    {
        var course = _adder.createCourse(dto);
        var status = _availability.CheckCoachCompetences(coach, course);

        if (status != StatusCourse.Assigned)
            return "Coach isn't available or competent for course";
        AllData.assignedCoaches.Add(coach);
        return $"Coach {coach.NameCoach} was added.";
    }
}