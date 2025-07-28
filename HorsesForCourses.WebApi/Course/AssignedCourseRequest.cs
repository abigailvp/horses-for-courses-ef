using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.WebApi.Factory;

public class AssignedCourseRequest
{
    public Guid CourseId { get; set; }
    public Guid coachId { get; set; }
}