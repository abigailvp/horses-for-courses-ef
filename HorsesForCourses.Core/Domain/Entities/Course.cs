using System.ComponentModel.DataAnnotations;
using HorsesForCourses.Core;
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

    public bool enoughTime { get; set; } = false;
    public bool coachAdded { get; set; } = false;

    public Dictionary<DateOnly, List<Timeslot>> CourseTimeslots = new Dictionary<DateOnly, List<Timeslot>>();
    public List<Competence> ListOfCourseCompetences = new List<Competence>();

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


    public StatusCourse AddTimeSlotToCourse(Timeslot availableMoment)
    {
        IEnumerable<DateOnly> onlyDates = CourseTimeslots.Select(c => c.Key);
        if (onlyDates.Contains(availableMoment.DayTimeslot))
        {
            DateOnly availableDate = availableMoment.DayTimeslot;
            IEnumerable<Timeslot> timeslotsWithSameDate = CourseTimeslots.Where(t => t.Key == availableDate).SelectMany(t => t.Value);
            bool hasSameBeginHours = timeslotsWithSameDate.Any(b => b.BeginTimeslot == availableMoment.BeginTimeslot);
            bool hasSameEndHours = timeslotsWithSameDate.Any(e => e.EndTimeslot == availableMoment.EndTimeslot);

            if (hasSameBeginHours && hasSameEndHours)
                return StatusCourse.PendingForTimeslots;
            else
            {
                var list = CourseTimeslots[availableDate];
                list.Add(availableMoment);
                return StatusCourse.WaitingForTimeslotCheck;
            }
        }
        else
        {
            List<Timeslot> TimeslotsPerDate = [availableMoment];
            CourseTimeslots.Add(availableMoment.DayTimeslot, TimeslotsPerDate);
            return StatusCourse.WaitingForTimeslotCheck;
        }
    }

    public StatusCourse ValidateCourseBasedOnTimeslots(StatusCourse status)
    {
        if (CourseTimeslots.Count == 0)
            return StatusCourse.PendingForTimeslots;
        var listOfTimeSlots = CourseTimeslots.SelectMany(x => x.Value);//SelectMany want je wilt maar 1 lijst (niet meerdere lijsten)
        enoughTime = listOfTimeSlots.Any(t => t.DurationTimeslot >= 1);

        if (enoughTime == true)
        {
            return StatusCourse.PendingForCompetenceCheck;
        }

        return StatusCourse.WaitingForTimeslotCheck;
    }

}