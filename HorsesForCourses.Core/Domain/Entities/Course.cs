using System.Diagnostics.SymbolStore;
using System.Security.Cryptography.X509Certificates;
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
    public List<Skill> ListOfCourseSkills = new();

    public bool hasSchedule { get; set; }
    public bool hasCoach { get; set; }

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

        hasSchedule = false;
        hasCoach = false;
    }


    public void AddCompetenceList(List<Skill> complist)
    {
        ListOfCourseSkills.Clear();
        foreach (Skill comp in complist)
        {
            ListOfCourseSkills.Add(comp);
        }
    }

    public void RemoveCompetence(Skill skill)
    {
        ListOfCourseSkills.Remove(skill);
    }


    public void AddTimeSlotToCourse(Timeslot moment) //start1 < end2 AND start2 < end1
    {
        IEnumerable<Timeslot> slots = CourseTimeslots.Where(c => c.DateTimeslot == moment.DateTimeslot);
        hasSchedule = slots.Any(c => c.BeginTimeslot < moment.EndTimeslot && c.EndTimeslot > moment.BeginTimeslot);
        if (!hasSchedule)
            CourseTimeslots.Add(moment);
    }

    public void AddTimeSlotList(List<Timeslot> timeList)
    {
        if (hasCoach == true)
            throw new NotReadyException("Can't change schedule when coach is already added");
        CourseTimeslots.Clear();
        foreach (Timeslot slot in timeList)
        {
            AddTimeSlotToCourse(slot);
        }
        hasSchedule = true;
    }

    public bool Overlaps(Timeslot slot)
    {
        return CourseTimeslots.Any(c => c.DateTimeslot == slot.DateTimeslot &&
                                c.BeginTimeslot < slot.EndTimeslot &&
                                c.EndTimeslot > slot.BeginTimeslot); //als dit ook waar is, is er sws overlap
    }

    public bool ConflictsWith(Coach coach)
    {
        var timeslotsCourseOfCoach = coach.ListOfCoursesAssignedTo.SelectMany(c => c.CourseTimeslots);

        foreach (Timeslot slot in timeslotsCourseOfCoach)
        {
            if (Overlaps(slot))
                return true;
        }
        return false;
    }


    public void ValidateCourseBasedOnTimeslots(Course course)
    {
        if (Availability.DoesCourseHaveTimeslots(course) == StatusCourse.WaitingForTimeslots)
            throw new NotReadyException("Course isn't ready yet");
    }

    public void CheckingCoach(Course course, Coach coach)
    {
        if (Availability.CheckingCoachByStatus(course, coach) == StatusCourse.WaitingForAvailableCoach ||
        Availability.CheckingCoachByStatus(course, coach) == StatusCourse.WaitingForCompetentCoach)
            throw new NotReadyException("Coach isn't suited for course");
        CoachForCourse = coach;
        coach.ListOfCoursesAssignedTo.Add(course);
        coach.numberOfAssignedCourses = +1;
        hasCoach = true;
    }

}