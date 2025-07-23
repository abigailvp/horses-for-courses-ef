using System.Net;
using System.Reflection.Metadata.Ecma335;
using HorsesForCourses.Core;
using Microsoft.VisualBasic;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.Core.DomainEntities;

public class Coach //aggregate root
{
    public Id<Coach> CoachId = new Id<Coach>(Guid.NewGuid());
    public string NameCoach { get; set; }
    public string Email { get; set; }

    public List<Competence> ListOfCompetences = new();
    public Dictionary<DateOnly, List<Timeslot>> AvailableTimeslots = new();

    public bool HasCourse = false;

    public Coach(string name, string email)
    {
        if (!email.Contains("@"))
            throw new ArgumentException("Email isn't valid.");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.");

        NameCoach = name;
        Email = email;

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

    public void AddTimeSlot(Timeslot availableMoment)
    {
        if (AvailableTimeslots.ContainsKey(availableMoment.DayTimeslot)) //coach
        {
            DateOnly availableDate = availableMoment.DayTimeslot;
            IEnumerable<Timeslot> timeslotsWithSameDate = AvailableTimeslots.Where(t => t.Key == availableDate).SelectMany(t => t.Value);
            bool hasSameBeginHours = timeslotsWithSameDate.Any(b => b.BeginTimeslot == availableMoment.BeginTimeslot);
            bool hasSameEndHours = timeslotsWithSameDate.Any(e => e.EndTimeslot == availableMoment.EndTimeslot);

            if (!hasSameBeginHours && !hasSameEndHours)
            {
                var list = AvailableTimeslots[availableDate];
                list.Add(availableMoment);
            }
        }
        else
        {
            List<Timeslot> TimeslotsPerDate = [availableMoment];
            AvailableTimeslots.Add(availableMoment.DayTimeslot, TimeslotsPerDate);
        }
    }

    public void RemoveTimeslot(Timeslot availableMoment)
    {
        var listOfThatDay = AvailableTimeslots[availableMoment.DayTimeslot];
        listOfThatDay.Remove(availableMoment);
    }

}





