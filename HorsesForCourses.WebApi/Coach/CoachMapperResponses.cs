using HorsesForCourses.WebApi.Factory;
using HorsesForCourses.Core.DomainEntities;
namespace HorsesForCourses.WebApi;

public static class CoachResponses
{

    public static ListOfCoachesResponse ConvertToListOfCoaches(List<Coach> listOfCoaches)
    {
        List<CoachResponse> lijstje = new();
        foreach (Coach coach in listOfCoaches)
        {
            CoachResponse response = new(coach.CoachId, coach.NameCoach, coach.Email, coach.numberOfAssignedCourses);
            lijstje.Add(response);
        }
        return new ListOfCoachesResponse { ListOfCoaches = lijstje };
    }

}

