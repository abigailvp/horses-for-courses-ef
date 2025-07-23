using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.Core;

namespace HorsesForCourses.Services;

public interface ICourseService
{
    string CreateEmptyCourse(CourseDTO dto);
    string CreateAndAssignCourse(Coach coach, CourseDTO dto);
}