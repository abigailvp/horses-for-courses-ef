using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.HorsesOnTheLoose;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.Core;

public class Availability
{

    public static StatusCourse DoesCourseHaveTimeslots(Course course)
    {
        if (course.CourseTimeslots.Count == 0)
            throw new NotReadyException("Course doesn't have timeslots");
        var enoughTime = course.CourseTimeslots.Any(t => t.DurationTimeslot >= 1);
        if (enoughTime == true)
            return StatusCourse.WaitingForCompetentCoach;
        return StatusCourse.WaitingForTimeslots;
    }

    public static StatusCourse CheckingCoachByStatus(Course course, Coach coach)
    {
        if (coach.ListOfCompetences.Count() == 0)
            throw new NotReadyException("Coach needs to have skills first");

        if (coach.ListOfCoursesAssignedTo.Count() > 0)
        {
            bool coachNotAvailable = course.ConflictsWith(coach);
            if (coachNotAvailable)
                throw new NotReadyException("Coach is not available");
        }


        var list = coach.ListOfCompetences;
        foreach (Skill required in course.ListOfCourseSkills)
        {
            bool matching = list.Any(c => c.Name == required.Name);
            if (!matching) //als er 1 is die niet dezelfde naam heeft
                throw new NotReadyException("Coach doesn't have necessary skills");
        }


        return StatusCourse.Assigned;
    }


}