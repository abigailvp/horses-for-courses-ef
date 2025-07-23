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
}