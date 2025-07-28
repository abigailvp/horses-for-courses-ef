using HorsesForCourses.WebApi.Factory;
using HorsesForCourses.Core.DomainEntities;
namespace HorsesForCourses.WebApi;

public static class CoachMapper
{
    public static CoachRequest ConvertToCoachDto(Coach coach)
        => new CoachRequest { CoachId = coach.CoachId.value, NameCoach = coach.NameCoach, Email = coach.Email };

    public static CompetentCoachRequest ConvertToCompetentCoach(Coach coach)
    => new CompetentCoachRequest { CoachId = coach.CoachId.value, ListOfCompetences = coach.ListOfCompetences };

    public static ScheduledCoachRequest ConvertToScheduledCoach(Coach coach)
    => new ScheduledCoachRequest { CoachId = coach.CoachId.value, CoachTimeslots = coach.AvailableTimeslots };
}