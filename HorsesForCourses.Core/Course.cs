using HorsesForCourses.Core;

namespace HorsesForCourses.Core;

public class Course
{
    public record Id<Course>(Guid Value);
    public string NameCourse { get; set; }
    public DateOnly StartDateCourse { get; set; }
    public DateOnly EndDateCourse { get; set; }
    public int DurationCourse { get; }
    public Coach CoachForCourse { get; }

    public bool enoughTime { get; set; }
    public bool coachAdded { get; set; }

    private List<Timeslot> CourseTimeslotsPerDate = new List<Timeslot>();
    public Dictionary<DateOnly, List<Timeslot>> CourseTimeslots = new Dictionary<DateOnly, List<Timeslot>>();
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
        CourseTimeslotsPerDate.Add(timeslot);
        CourseTimeslots.Add(timeslot.DayTimeslot, CourseTimeslotsPerDate);
    }

    public StatusCourse ValidateCourseBasedOnTimeslots()
    {
        if (CourseTimeslots.Count == 0)
            return StatusCourse.PendingForTimeslots;
        var listOfTimeSlots = CourseTimeslots.SelectMany(x => x.Value);//SelectMany want je wilt maar 1 lijst (niet meerdere lijsten)
        enoughTime = listOfTimeSlots.Any(t => t.DurationTimeslot >= 1);

        if (enoughTime == true)
        {
            return StatusCourse.PendingForCoach;
        }

        return StatusCourse.PendingForTimeslots;
    }

    // public void AddCoach(Coach coach)
    // {
    //     if(CheckAvailability(course) == true)
    //     CoachForCourse = coach;
    // }

}