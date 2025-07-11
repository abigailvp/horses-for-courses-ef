using HorsesForCourses.Core;

namespace HorsesForCourses.Course;

public class Course
{
    public record Id<Course>(Guid Value);
    public string NameCourse { get; set; }
    public DateOnly StartDateCourse { get; set; }
    public DateOnly EndDateCourse { get; set; }
    public int DurationCourse { get; }

    public readonly Dictionary<Weekdays, Timeslot> CourseTimeslots = new Dictionary<Weekdays, Timeslot>();
    private readonly List<Competence> ListOfCourseCompetences = new List<Competence>();

    public Course(string nameCourse, DateOnly startcourse, DateOnly endcourse)
    {
        NameCourse = nameCourse;
        if (endcourse.DayNumber - startcourse.DayNumber > 0) //.DayNumber for duration
            StartDateCourse = startcourse;
        EndDateCourse = endcourse;
    }

    public void AddRequiredCompetentence(Competence comp)
    {
        ListOfCourseCompetences.Add(comp);
    }


    public void AddTimeslotToCourse(Timeslot timeslot)
    {
        CourseTimeslots.Add(timeslot.WeekdayTimeslot, timeslot);
    }

    public StatusCourse ValidateCourseBasedOnTimeslots()
    {
        if (CourseTimeslots.Count == 0)
            return StatusCourse.PendingForTimeslots;
        var onlyTimeslots = CourseTimeslots.Select(x => x.Value);
        bool enoughTime = onlyTimeslots.Any(t => t.DurationTimeslot >= 1);

        if (enoughTime == true)
        {
            return StatusCourse.PendingForCoach;
        }

        return StatusCourse.PendingForTimeslots;
    }

}