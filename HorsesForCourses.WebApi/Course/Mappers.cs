using HorsesForCourses.WebApi.Factory;
using HorsesForCourses.Core.DomainEntities;
namespace HorsesForCourses.WebApi;

public static class CourseMapper
{
    public static CourseRequest ConvertToCourseDto(Course course)
  => new CourseRequest { NameCourse = course.NameCourse, StartDateCourse = course.StartDateCourse.ToString(), EndDateCourse = course.EndDateCourse.ToString() };

    public static CompetentCourseRequest ConvertToCompetentCourse(Course course)
    => new CompetentCourseRequest { CourseId = course.CourseId.value, ListOfCourseCompetences = course.ListOfCourseCompetences };

    public static ScheduledCourseRequest ConvertToScheduledCourse(Course course)
    => new ScheduledCourseRequest { CourseId = course.CourseId.value, CourseTimeslots = course.CourseTimeslots };

    public static AssignedCourseRequest ConvertToAssignedCourse(Course course, Coach coach)
   => new AssignedCourseRequest { CourseId = course.CourseId.value, coachId = coach.CoachId.value };
}