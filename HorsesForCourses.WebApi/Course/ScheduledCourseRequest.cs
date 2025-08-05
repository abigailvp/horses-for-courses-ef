using System.ComponentModel.DataAnnotations;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.WebApi.Factory;

public class ScheduledCourseRequest
{
    [Required]
    public List<MyTimeslot> CourseTimeslots { get; set; }

}

public record MyTimeslot(string Day, int beginhour, int endhour);