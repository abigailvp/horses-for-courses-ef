using HorsesForCourses.Core.DomainEntities;

public interface IAdding
{
    Coach createCoach(CoachDTO coach);
    Course createCourse(CourseDTO course);
}