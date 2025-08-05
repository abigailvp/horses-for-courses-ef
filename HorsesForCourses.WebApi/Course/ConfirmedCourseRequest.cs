using System.ComponentModel.DataAnnotations;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.WebApi.Factory;

public class ConfirmedCourseRequest
{
    [Required]
    public List<Timeslot> Timeslots { get; set; }



}