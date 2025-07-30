using System.Diagnostics.SymbolStore;
using HorsesForCourses.Core.HorsesOnTheLoose;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.Core.DomainEntities;

public class Course
{
    public int CourseId { get; set; }
    public string NameCourse { get; set; }
    public DateOnly StartDateCourse { get; set; }
    public DateOnly EndDateCourse { get; set; }
    public int DurationCourse { get; }
    public Coach CoachForCourse { get; set; }

    public List<Timeslot> CourseTimeslots = new();
    public List<Competence> ListOfCourseCompetences = new();

    public Course(string nameCourse, DateOnly startcourse, DateOnly endcourse)
    {
        if (string.IsNullOrWhiteSpace(nameCourse))
            throw new DomainException("Must give course a name");
        NameCourse = nameCourse;
        if (endcourse.DayNumber - startcourse.DayNumber <= 0) //.DayNumber for duration
            throw new DomainException("Startdate and enddate don't make sense");
        StartDateCourse = startcourse;
        EndDateCourse = endcourse;
        CourseId = new int();
    }

    public void AddRequiredCompetentence(string name)
    {
        Competence comp = new Competence(name);
        ListOfCourseCompetences.Add(comp);
    }

    public string AddCompetenceList(List<Competence> complist)
    {
        ListOfCourseCompetences.Clear();
        foreach (Competence comp in complist)
        {
            ListOfCourseCompetences.Add(comp);
        }
        return $"Course has new required competences list";
    }


    public void AddTimeSlotToCourse(Timeslot moment) //start1 < end2 AND start2 < end1
    {
        IEnumerable<Timeslot> slots = CourseTimeslots.Where(c => c.DayTimeslot == moment.DayTimeslot);
        bool hasSameTime = slots.Any(c => c.BeginTimeslot < moment.EndTimeslot && c.EndTimeslot > moment.BeginTimeslot);
        if (!hasSameTime)
            CourseTimeslots.Add(moment);
    }

    public string AddTimeSlotList(List<Timeslot> timeList)
    {
        CourseTimeslots.Clear();
        foreach (Timeslot slot in timeList)
        {
            AddTimeSlotToCourse(slot);
        }
        return $"Course now has new timeslotlist";
    }

    public void ValidateCourseBasedOnTimeslots(Course course)
    {
        if (Availability.DoesCourseHaveTimeslots(course) == StatusCourse.WaitingForTimeslots)
            throw new NotReadyException("Course isn't ready yet");
    }

    public void CheckingCoach(Course course, Coach coach)
    {
        if (Availability.CheckingCoachByStatus(course, coach) == StatusCourse.WaitingForMatchingTimeslots)
            throw new NotReadyException("Coach isn't suited for course");
        CoachForCourse = coach;
        coach.ListOfCoursesAssignedTo.Add(course);
        coach.numberOfAssignedCourses = +1;
    }

}