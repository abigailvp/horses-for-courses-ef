using HorsesForCourses.Core.DomainEntities;

namespace HorsesForCourses.WebApi.Factory;


public class ListOfCoachesResponse
{
    public List<CoachResponse> ListOfCoaches { get; set; }
}

public record CoachResponse(int id, string name, string email, int numberOfAssignedCourses);


