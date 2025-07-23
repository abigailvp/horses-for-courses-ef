using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core;

public class Adding
{
    public List<Coach> addCoach(StatusCourse status, Coach coach)
    {
        if (status == StatusCourse.Assigned)
            AllData.allCoaches.Add(coach);
        return AllData.allCoaches;
    }

    public List<Course> addCourse(StatusCourse status, Course course)
    {
        if (status == StatusCourse.PendingForCompetenceCheck)
            AllData.allCourses.Add(course);
        return AllData.allCourses;
    }
}