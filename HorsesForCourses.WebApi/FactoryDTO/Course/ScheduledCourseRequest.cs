using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.WebApi.Factory;

public class ScheduledCourseRequest
{
    public Id<Course> CourseId { get; set; }
    public List<Timeslot> CourseTimeslots { get; set; }
}