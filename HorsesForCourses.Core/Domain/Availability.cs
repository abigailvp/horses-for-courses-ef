using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.Services;

public class Availability
{

    public static StatusCourse ValidateCourseBasedOnTimeslots(Course course)
    {
        if (course.CourseTimeslots.Count == 0)
            return StatusCourse.WaitingForTimeslots;
        var enoughTime = course.CourseTimeslots.Any(t => t.DurationTimeslot >= 1);
        if (enoughTime == true)
            return StatusCourse.WaitingForMatchingTimeslots;
        return StatusCourse.WaitingForTimeslots;
    }

    public static StatusCourse CheckCoachAvailability(Course course, Coach coach)
    {
        //lege lijst voor matchende timeslots
        List<Timeslot> matchingTimeslots = new();

        // zelfde dagen van course en coach 
        foreach (Timeslot slot in course.CourseTimeslots)
        {
            var sameDayTimeSlots = coach.AvailableTimeslots.Where(c => c.DayTimeslot == slot.DayTimeslot);
            foreach (Timeslot timeslot in sameDayTimeSlots)
            { matchingTimeslots.Add(timeslot); }

        }
        if (!matchingTimeslots.Any())
            return StatusCourse.WaitingForMatchingTimeslots;


        // overlap checken
        foreach (Timeslot slot in matchingTimeslots)
        {
            var hasOverlap = coach.AvailableTimeslots.Any(c => c.BeginTimeslot < slot.EndTimeslot && c.EndTimeslot > slot.BeginTimeslot);
            if (!hasOverlap)
                return StatusCourse.WaitingForMatchingTimeslots;

        }
        return StatusCourse.WaitingForMatchingCompetences;
    }

    public static StatusCourse CheckCoachCompetencesForCourse(Course course, Coach coach)
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

    public static StatusCourse CheckingCoach(Course course, Coach coach)
    {
        var status = CheckCoachAvailability(course, coach);
        if (status != StatusCourse.WaitingForMatchingCompetences)
            return StatusCourse.WaitingForMatchingTimeslots;
        var statusTwo = CheckCoachCompetencesForCourse(course, coach);
        if (statusTwo != StatusCourse.Assigned)
            return StatusCourse.WaitingForMatchingCompetences;
        return StatusCourse.Assigned;
    }
}