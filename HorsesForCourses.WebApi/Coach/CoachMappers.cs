using HorsesForCourses.WebApi.Factory;
using HorsesForCourses.Core.DomainEntities;
namespace HorsesForCourses.WebApi;

public static class CoachMapper
{
    public static CoachRequest ConvertToCoachDto(Coach coach)
        => new CoachRequest { NameCoach = coach.NameCoach, Email = coach.Email };

    public static CompetentCoachRequest ConvertToCompetentCoach(Coach coach)
    => new CompetentCoachRequest { ListOfCompetences = coach.ListOfCompetences };

    public static ScheduledCoachRequest ConvertToScheduledCoach(Coach coach)
    => new ScheduledCoachRequest { CoachTimeslots = coach.AvailableTimeslots };

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

    public static DetailedCoachResponse ConvertToDetailedCoach(Coach coach)
    {
        List<AssignedCourse> assignedCourses = new();
        foreach (Course course in coach.ListOfCoursesAssignedTo)
        {
            AssignedCourse aCourse = new(course.CourseId, course.NameCourse);
            assignedCourses.Add(aCourse);
        }
        return new DetailedCoachResponse { Id = coach.CoachId, Name = coach.NameCoach, Email = coach.Email, ListOfCompetences = coach.ListOfCompetences, ListOfAssignedCourses = assignedCourses };
    }
}

// [{ 
//         "id": int, 
//         "name": string, 
//         "email": string,
//         "numberOfCoursesAssignedTo": int
//     }] 
// }
// 📄 Get / coaches /{ id}
// Request:
// Url parameter: Id: int
// Response:
// JSON body: string
// 