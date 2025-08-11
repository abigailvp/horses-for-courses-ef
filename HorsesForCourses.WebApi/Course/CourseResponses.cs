using HorsesForCourses.WebApi.Factory;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
namespace HorsesForCourses.WebApi;

public static class CourseResponses
{

  public static AllCoursesResponse ConvertToListCourses(List<Course> list)
  {
    List<CourseResponse> courseResponses = new();
    foreach (Course course in list)
    {
      CourseResponse response = new(course.CourseId, course.NameCourse, course.StartDateCourse.ToString(), course.EndDateCourse.ToString(), course.hasSchedule, course.hasCoach);
      courseResponses.Add(response);
    }
    return new AllCoursesResponse { ListOfCourses = courseResponses };
  }

  public static DetailedCourseResponse ConvertToDetailedCourse(Course course)
  => new DetailedCourseResponse
  {
    Id = course.CourseId,
    Name = course.NameCourse,
    startDate = course.StartDateCourse.ToString(),
    endDate = course.EndDateCourse.ToString(),
    skills = course.ListOfCourseSkills,
    ListOfTimeslots = CourseMapper.ConvertToScheduledCourse(course).CourseTimeslots,
    coach = new CoachForCourseResponse(course.CoachId, course.CoachForCourse.NameCoach)
  };

}
