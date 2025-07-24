using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.Core.DomainEntities;

public class Course
{
    public Id<Course> CourseId { get; set; }
    public string NameCourse { get; set; }
    public DateOnly StartDateCourse { get; set; }
    public DateOnly EndDateCourse { get; set; }
    public int DurationCourse { get; }
    public Coach CoachForCourse { get; set; }

    public Dictionary<DateOnly, List<Timeslot>> CourseTimeslots = new();
    public List<Competence> ListOfCourseCompetences = new();

    public Course(string nameCourse, DateOnly startcourse, DateOnly endcourse)
    {
        NameCourse = nameCourse;
        if (endcourse.DayNumber - startcourse.DayNumber > 0) //.DayNumber for duration
            StartDateCourse = startcourse;
        EndDateCourse = endcourse;
        CourseId = new Id<Course>(Guid.NewGuid());
    }

    public void AddRequiredCompetentence(string name, int level)
    {
        Competence comp = new Competence(name, level);
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


    public void AddTimeSlotToCourse(Timeslot availableMoment)
    {
        IEnumerable<DateOnly> onlyDates = CourseTimeslots.Select(c => c.Key);
        if (onlyDates.Contains(availableMoment.DayTimeslot))
        {
            DateOnly availableDate = availableMoment.DayTimeslot;
            IEnumerable<Timeslot> timeslotsWithSameDate = CourseTimeslots.Where(t => t.Key == availableDate).SelectMany(t => t.Value);
            bool hasSameBeginHours = timeslotsWithSameDate.Any(b => b.BeginTimeslot == availableMoment.BeginTimeslot);
            bool hasSameEndHours = timeslotsWithSameDate.Any(e => e.EndTimeslot == availableMoment.EndTimeslot);

            if (!hasSameBeginHours && !hasSameEndHours)
            {
                var list = CourseTimeslots[availableDate];
                list.Add(availableMoment);
            }
        }
        else
        {
            List<Timeslot> TimeslotsPerDate = [availableMoment];
            CourseTimeslots.Add(availableMoment.DayTimeslot, TimeslotsPerDate);
        }
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


}