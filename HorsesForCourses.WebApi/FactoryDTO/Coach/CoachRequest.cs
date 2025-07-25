using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;


namespace HorsesForCourses.WebApi.Factory;


public class CoachRequest
{
    public Guid CoachId { get; set; }
    public string NameCoach { get; set; }
    public string Email { get; set; }

    //geen lijst met competenties of timeslots want zit in domein
}


