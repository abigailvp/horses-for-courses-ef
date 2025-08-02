using HorsesForCourses.WebApi.Factory;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
namespace HorsesForCourses.WebApi;

public static class CourseMapper
{
  public static CourseRequest ConvertToCourseDto(Course course)
=> new CourseRequest { NameCourse = course.NameCourse, StartDateCourse = course.StartDateCourse.ToString(), EndDateCourse = course.EndDateCourse.ToString() };

  public static CompetentCourseRequest ConvertToCompetentCourse(Course course)
  => new CompetentCourseRequest { ListOfCourseCompetences = course.ListOfCourseSkills };

  public static List<Timeslot> ConvertToDomainList(List<MyTimeslot> list)
  {
    List<Timeslot> realList = new();
    foreach (MyTimeslot slot in list)
    {
      Timeslot realSlot = new(slot.beginhour, slot.endhour, DateOnly.Parse(slot.Day)); //tryparse is veiliger als cliënt verkeerd ingeeft
      realList.Add(realSlot);
    }
    return realList;
  }

  public static ScheduledCourseRequest ConvertToScheduledCourse(Course course)
  {
    List<MyTimeslot> lijst = new();
    foreach (Timeslot slot in course.CourseTimeslots)
    {
      MyTimeslot shortSlot = new(slot.DateTimeslot.DayOfWeek.ToString(), slot.BeginTimeslot, slot.EndTimeslot);
      lijst.Add(shortSlot);
    }
    return new ScheduledCourseRequest { CourseTimeslots = lijst };
  }

  public static AssignedCourseRequest ConvertToAssignedCourse(Coach coach)
 => new AssignedCourseRequest { coachId = coach.CoachId };

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
    ListOfTimeslots = ConvertToScheduledCourse(course).CourseTimeslots,
    coach = new CoachForCourseResponse(course.CoachForCourse.CoachId, course.CoachForCourse.NameCoach)
  };

}
