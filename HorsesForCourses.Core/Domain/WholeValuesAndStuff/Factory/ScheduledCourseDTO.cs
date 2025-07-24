using HorsesForCourses.Core.DomainEntities;

namespace HorsesForCourses.Core.WholeValuesAndStuff;

public class ScheduledCourseDTO
{
    public Id<Course> CourseId { get; set; }
    public List<Timeslot> CourseTimeslots { get; set; }
}