using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.WebApi.Factory;

public class ConfirmedCourseRequest
{
    public Guid CourseId { get; set; }
    public List<Timeslot> Timeslots { get; set; }

}