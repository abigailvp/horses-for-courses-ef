using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.Services;

public class Availability
{

    public static StatusCourse ValidateCourseBasedOnTimeslots(Course course)
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


    public StatusCourse CheckCoachCompetencesForCourse(Coach coach, Course course)
    {
        var list = coach.ListOfCompetences;

        foreach (var required in course.ListOfCourseCompetences)
        {
            bool matching = list.All(c => c.Name == required.Name && c.Level >= required.Level);
            if (!matching)
                return StatusCourse.WaitingForMatchingCompetences;
        }
        course.CoachForCourse = coach;
        return StatusCourse.Assigned;
    }

    public StatusCourse CheckCoachAvailability(Course course, Coach coach)
    {
        //condition1: samedates in course and coach
        var courseTimeslots = course.CourseTimeslots;
        var onlyMatchingDates = courseTimeslots.Join(coach.AvailableTimeslots,//list for coach
                                c => c.Key,//sleutel=datum
                                a => a.Key,
                                (c, a) => new { courseTimeslot = c.Value, coachTimeslot = a.Value });
        IEnumerable<List<Timeslot>> numberOfMatches = onlyMatchingDates.Select(x => x.courseTimeslot);
        if (courseTimeslots.Keys.Count() != numberOfMatches.Count())
            return StatusCourse.WaitingForMatchingTimeslots;


        foreach (var matchingDates in onlyMatchingDates)
        {
            //condition 2: PER DAY: coachslot has starthour <= courselot && endhour>=courselot
            foreach (var courseSlot in matchingDates.courseTimeslot)
            {

                bool coachCovers = matchingDates.coachTimeslot.Any(coachSlot =>
                                    coachSlot.BeginTimeslot <= courseSlot.BeginTimeslot &&
                                    coachSlot.EndTimeslot >= courseSlot.EndTimeslot);

                if (!coachCovers)
                    return StatusCourse.WaitingForMatchingTimeslots;
            }
        }

        return StatusCourse.WaitingForMatchingCompetences;
    }

}