using System.ComponentModel.DataAnnotations;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.WebApi.Factory;

public class CompetentCoachRequest
{
    [Required]
    public List<Skill> ListOfSkills { get; set; }
}