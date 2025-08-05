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
    public int CoachId { get; set; } //ef stelt deze in
    public List<Timeslot> CourseTimeslots = new();
    public List<Skill> ListOfCourseSkills = new();

    public StatusCourse Status { get; set; }
    public bool hasSchedule { get; set; }
    public bool hasCoach { get; set; }

    public Course() { } //EF heeft deze nodig voor migrations uit te voeren

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

        Status = StatusCourse.Created;
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
        hasSchedule = Overlaps(moment);
        if (!hasSchedule)
            CourseTimeslots.Add(moment);
    }

    public void RemoveTimeSlot(Timeslot moment)
    {
        bool hasMoment = CourseTimeslots.Any(c => c.DateTimeslot == moment.DateTimeslot &&
                                                    c.BeginTimeslot == moment.BeginTimeslot &&
                                                    c.EndTimeslot == moment.EndTimeslot);
        if (hasMoment)
            CourseTimeslots.Remove(moment);
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
                                c.EndTimeslot > slot.BeginTimeslot &&
                                c.DurationTimeslot <= slot.DurationTimeslot); //als dit ook waar is, is er sws overlap
    }

    public bool ConflictsWith(Coach coach)
    {
        var timeslotsCoach = coach.ListOfCoursesAssignedTo.SelectMany(c => c.CourseTimeslots);

        foreach (Timeslot slot in timeslotsCoach)
        {
            if (Overlaps(slot))
                return true;
        }
        return false;
    }


    public void ValidateCourseBasedOnTimeslots(Course course)
    {
        Status = Availability.DoesCourseHaveTimeslots(course);
        hasSchedule = true;
    }

    public void AddingCoach(Course course, Coach coach)
    {
        ValidateCourseBasedOnTimeslots(course);
        Status = Availability.CheckingCoachByStatus(course, coach);

        if (Status == StatusCourse.Assigned)
        {
            CoachForCourse = coach;
            coach.ListOfCoursesAssignedTo.Add(course);
            coach.numberOfAssignedCourses += 1;
            hasCoach = true;
        }
    }
}