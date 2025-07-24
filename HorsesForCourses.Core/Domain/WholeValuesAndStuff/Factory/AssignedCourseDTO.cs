using HorsesForCourses.Core.DomainEntities;

namespace HorsesForCourses.Core.WholeValuesAndStuff;

public class AssignedCourseDTO
{
    public Id<Course> CourseId { get; set; }
    public List<Timeslot> CourseTimeslots { get; set; }
    public Coach coach { get; set; }
}