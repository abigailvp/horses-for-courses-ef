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

    public static DetailedCoachResponse ConvertToDetailedCoach(Coach coach)
    {
        List<AssignedCourse> assignedCourses = new();
        foreach (Course course in coach.ListOfCoursesAssignedTo)
        {
            AssignedCourse aCourse = new(course.CourseId, course.NameCourse);
            assignedCourses.Add(aCourse);
        }
        return new DetailedCoachResponse { Id = coach.CoachId, Name = coach.NameCoach, Email = coach.Email, ListOfSkills = coach.ListOfCompetences, ListOfAssignedCourses = assignedCourses };
    }
}

