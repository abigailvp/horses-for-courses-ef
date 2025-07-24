using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.Services;

public class Availability
{
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


    public StatusCourse CheckCoachCompetences(Coach coach, Course course)
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
}