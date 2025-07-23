using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

using HorsesForCourses.Core;

public interface IAdding
{
    Coach createCoach(CoachDTO coach);
    Course createCourse(CourseDTO course);
}