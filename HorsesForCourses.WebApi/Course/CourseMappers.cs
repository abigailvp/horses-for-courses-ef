using HorsesForCourses.WebApi.Factory;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
namespace HorsesForCourses.WebApi;

public static class CourseMapper
{


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



}
