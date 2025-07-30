using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.WebApi.Factory;

public class ScheduledCoachRequest
{
    public List<Timeslot> CoachTimeslots { get; set; }
}