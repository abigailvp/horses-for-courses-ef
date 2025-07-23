using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.Core;

namespace HorsesForCourses.Services;

public class TimeslotService
{
    public StatusCourse ValidateCourseBasedOnTimeslots(Course course, Timeslot slot)
    {
        if (course.CourseTimeslots.Count == 0)
            return StatusCourse.WaitingForTimeslots;
        var listOfTimeSlots = course.CourseTimeslots.SelectMany(x => x.Value);//SelectMany want je wilt maar 1 lijst (niet meerdere lijsten)
        var enoughTime = listOfTimeSlots.Any(t => t.DurationTimeslot >= 1);

        if (enoughTime == true)
        {
            return StatusCourse.WaitingForMatchingTimeslots;
        }
        return StatusCourse.WaitingForTimeslots;
    }


}
