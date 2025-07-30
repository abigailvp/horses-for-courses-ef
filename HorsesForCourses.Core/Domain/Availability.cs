using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.HorsesOnTheLoose;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.Core;

public class Availability
{

    public static StatusCourse DoesCourseHaveTimeslots(Course course)
    {
        if (course.CourseTimeslots.Count == 0)
            return StatusCourse.WaitingForTimeslots;
        var enoughTime = course.CourseTimeslots.Any(t => t.DurationTimeslot >= 1);
        if (enoughTime == true)
            return StatusCourse.WaitingForAvailableCoach;
        return StatusCourse.WaitingForTimeslots;
    }



    private static StatusCourse CheckCoachAvailability(Course course, Coach coach)
    {
        if (coach.numberOfAssignedCourses == 0)
            return StatusCourse.WaitingForAvailableCoach;

        bool conflict = course.ConflictsWith(coach);
        if (conflict)
            return StatusCourse.WaitingForAvailableCoach;
        return StatusCourse.WaitingForCompetentCoach;

    }

    private static StatusCourse CheckCoachCompetencesForCourse(Course course, Coach coach)
    {
        var list = coach.ListOfCompetences;

        foreach (var required in course.ListOfCourseSkills)
        {
            bool matching = list.All(c => c.Name == required.Name);
            if (!matching)
                return StatusCourse.WaitingForCompetentCoach;
        }
        return StatusCourse.Assigned;
    }

    public static StatusCourse CheckingCoachByStatus(Course course, Coach coach)
    {
        var status = CheckCoachAvailability(course, coach);
        if (status != StatusCourse.WaitingForCompetentCoach)
            return StatusCourse.WaitingForAvailableCoach;
        var statusTwo = CheckCoachCompetencesForCourse(course, coach);
        if (statusTwo != StatusCourse.Assigned)
            return StatusCourse.WaitingForCompetentCoach;
        return StatusCourse.Assigned;
    }


}