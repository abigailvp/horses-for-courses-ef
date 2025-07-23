using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.Core;

namespace HorsesForCourses.Services;

public interface ICoachService
{
    string CreateAndAssignCoach(Course course, CoachDTO dto);
}