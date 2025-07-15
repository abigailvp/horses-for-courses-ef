using System.Net;
using HorsesForCourses.Core;
using Microsoft.VisualBasic;

namespace HorsesForCourses.Core;

public class Coach //aggregate root
{
    public Id<Coach> CoachId { get; }
    public string NameCoach { get; }
    public string Email { get; }

    private readonly List<Competence> ListOfCompetences = new List<Competence>();
    public readonly Dictionary<DateOnly, List<Timeslot>> AvailableTimeslots = new Dictionary<DateOnly, List<Timeslot>>();

    private Coach(Id<Coach> coachid, string name, string email)
    {
        NameCoach = name;
        Email = email;
        CoachId = coachid;
    }
    public Coach AddCoach(Id<Coach> value, string name, string email)
    {
        if (!email.Contains("@"))
            throw new ArgumentException("Email isn't valid.");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.");
        var newCoach = new Coach(value, name, email);

        return newCoach;
    }

    public void AddCompetence(string name, int level)
    {
        var newCompetence = new Competence(name, level);
        ListOfCompetences.Add(newCompetence);
    }

    public void RemoveCompetence(Competence competence)
    {
        ListOfCompetences.Remove(competence);
    }

    public StatusCourse AddTimeSlot(Timeslot availableMoment)
    {
        IEnumerable<DateOnly> onlyDates = AvailableTimeslots.Select(c => c.Key);
        if (onlyDates.Contains(availableMoment.DayTimeslot))
        {
            DateOnly availableDate = availableMoment.DayTimeslot;
            IEnumerable<Timeslot> timeslotsWithSameDate = AvailableTimeslots.Where(t => t.Key == availableDate).SelectMany(t => t.Value);
            bool hasSameBeginHours = timeslotsWithSameDate.Any(b => b.BeginTimeslot == availableMoment.BeginTimeslot);
            bool hasSameEndHours = timeslotsWithSameDate.Any(e => e.EndTimeslot == availableMoment.EndTimeslot);

            if (hasSameBeginHours && hasSameEndHours)
                return StatusCourse.PendingForTimeslots;
            else
            {
                var list = AvailableTimeslots[availableDate];
                list.Add(availableMoment);
                AvailableTimeslots.Add(availableMoment.DayTimeslot, list);
                return StatusCourse.WaitingForTimeslotCheck;
            }
        }
        else
        {
            List<Timeslot> TimeslotsPerDate = new List<Timeslot>();
            TimeslotsPerDate.Add(availableMoment);
            AvailableTimeslots.Add(availableMoment.DayTimeslot, TimeslotsPerDate);
            return StatusCourse.WaitingForTimeslotCheck;
        }
    }

    public void RemoveTimeslot(Timeslot availableMoment)
    {
        AvailableTimeslots.Remove(availableMoment.DayTimeslot);
    }

    public StatusCourse CheckAvailability(Course course)
    {
        //condition1: samedates in course and coach
        var courseTimeslots = course.CourseTimeslots;
        var onlyMatchingDates = courseTimeslots.Join(AvailableTimeslots,//list for coach
                                c => c.Key,//sleutel=datum
                                a => a.Key,
                                (c, a) => new { courseTimeslot = c.Value, coachTimeslot = a.Value });
        IEnumerable<List<Timeslot>> numberOfMatches = onlyMatchingDates.Select(x => x.courseTimeslot);
        if (courseTimeslots.Keys.Count() != numberOfMatches.Count())
            return StatusCourse.WaitingForTimeslotCheck;


        foreach (var matchingDates in onlyMatchingDates)
        {
            //condition 2: PER DAY: coachslot has starthour <= courselot && endhour>=courselot
            foreach (var courseSlot in matchingDates.courseTimeslot)
            {

                bool coachCovers = matchingDates.coachTimeslot.Any(coachSlot =>
                                    coachSlot.BeginTimeslot <= courseSlot.BeginTimeslot &&
                                    coachSlot.EndTimeslot >= courseSlot.EndTimeslot);

                if (!coachCovers)
                    return StatusCourse.WaitingForTimeslotCheck;
            }
        }

        return StatusCourse.PendingForCoach;
    }
}





