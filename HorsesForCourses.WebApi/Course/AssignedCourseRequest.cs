using System.ComponentModel.DataAnnotations;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.WebApi.Factory;

public class AssignedCourseRequest
{
    [Required] //wordt automatisch key
    [Key]
    public int coachId { get; set; }



}