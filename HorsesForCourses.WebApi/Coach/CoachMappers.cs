using HorsesForCourses.WebApi.Factory;
using HorsesForCourses.Core.DomainEntities;
namespace HorsesForCourses.WebApi;

public static class CoachMapper
{
    public static CoachRequest ConvertToCoachDto(Coach coach)
        => new CoachRequest { NameCoach = coach.NameCoach, Email = coach.Email };

    public static CompetentCoachRequest ConvertToCompetentCoach(Coach coach)
    => new CompetentCoachRequest { ListOfSkills = coach.ListOfCompetences };

    public static ScheduledCoachRequest ConvertToScheduledCoach(Coach coach)
    => new ScheduledCoachRequest { CoachTimeslots = coach.AvailableTimeslots };


}

