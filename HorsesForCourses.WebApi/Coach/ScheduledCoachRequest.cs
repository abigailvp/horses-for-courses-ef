using System.ComponentModel.DataAnnotations;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.WebApi.Factory;

public class ScheduledCoachRequest
{
    [Required]
    public List<Timeslot> CoachTimeslots { get; set; }


}