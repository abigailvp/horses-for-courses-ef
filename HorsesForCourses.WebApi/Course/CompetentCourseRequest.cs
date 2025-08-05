using System.ComponentModel.DataAnnotations;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.WebApi.Factory;

public class CompetentCourseRequest
{
    [Required]
    public List<Skill> ListOfCourseCompetences { get; set; }



}