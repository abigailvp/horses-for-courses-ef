using HorsesForCourses.Core;
using HorsesForCourses.Core.DomainEntities;

public class CoachDTO : ICoachDTO
{
    public Guid CoachId { get; set; }
    public string NameCoach { get; set; }
    public string Email { get; set; }

    public string CreateCoach(CoachDTO dto)
    {
        Coach coach = new(dto.NameCoach, dto.Email)
        {
            CoachId = new Id<Coach>(dto.CoachId)
        };
        AllData.allCoaches.Add(coach);
        return $"Coach has the name {coach.NameCoach} and can be reached at {coach.Email}.";
    }

}