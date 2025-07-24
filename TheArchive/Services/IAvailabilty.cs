using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

public interface IAvailability
{
    StatusCourse CheckCoachAvailability(Course course, Coach coach);
    // StatusCourse CheckCoach(Coach coach, Course course);
    StatusCourse CheckCoachCompetences(Coach coach, Course course);
    // void AssignCourse(StatusCourse status, Course course, Coach coach);
}