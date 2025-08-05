using System.ComponentModel.DataAnnotations;

namespace HorsesForCourses.WebApi.Factory;


public class CoachRequest
{
    [Required]
    [MaxLength(100)]
    public string NameCoach { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    //geen lijst met competenties of timeslots want zit in domein

}


