using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.WebApi.Factory;

public class ScheduledCoachRequest
{
    public Guid CoachId { get; set; }
    public List<Timeslot> CoachTimeslots { get; set; }
}