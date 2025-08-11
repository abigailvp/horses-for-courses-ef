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


}
