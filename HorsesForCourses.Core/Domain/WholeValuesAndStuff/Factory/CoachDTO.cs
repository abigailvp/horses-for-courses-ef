using HorsesForCourses.Core.DomainEntities;

namespace HorsesForCourses.Core.WholeValuesAndStuff;


public class CoachDTO
{
    public Guid CoachId { get; set; }
    public string NameCoach { get; set; }
    public string Email { get; set; }

    //geen lijst met competenties of timeslots want zit in domein
}

public class CoachMapper
{
    public static Coach CreateCoach(CoachDTO dto)
    {
        return new Coach(dto.NameCoach, dto.Email)
        {
            CoachId = new Id<Coach>(dto.CoachId)
        };
    }
}
