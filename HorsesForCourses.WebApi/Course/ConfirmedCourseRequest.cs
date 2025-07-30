using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.WebApi.Factory;

public class ConfirmedCourseRequest
{
    public List<Timeslot> Timeslots { get; set; }

}