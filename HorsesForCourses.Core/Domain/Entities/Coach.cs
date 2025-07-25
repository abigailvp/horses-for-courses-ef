using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.Core.HorsesOnTheLoose;

namespace HorsesForCourses.Core.DomainEntities;

public class Coach //aggregate root
{
    public Id<Coach> CoachId { get; set; }
    public string NameCoach { get; set; }
    public string Email { get; set; }

    public List<Competence> ListOfCompetences = new();
    public List<Timeslot> AvailableTimeslots = new();

    public bool HasCourse = false;

    public Coach(string name, string email)
    {
        if (!email.Contains("@"))
            throw new DomainException("Email isn't valid.");
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email cannot be empty.");
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name can't be empty");
        NameCoach = name;
        Email = email;
        CoachId = new Id<Coach>(Guid.NewGuid()); //pas aanmaken als je een coach aanmaakt

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

    public string AddCompetenceList(List<Competence> complist)
    {
        ListOfCompetences.Clear();
        foreach (Competence comp in complist)
        {
            ListOfCompetences.Add(comp);
        }
        return $"Coach has new competences list";
    }

    public void AddTimeSlot(Timeslot moment)
    {
        IEnumerable<Timeslot> slots = AvailableTimeslots.Where(c => c.DayTimeslot == moment.DayTimeslot);
        bool hasSameTime = slots.Any(c => c.BeginTimeslot < moment.EndTimeslot && c.EndTimeslot > moment.BeginTimeslot);
        if (!hasSameTime)
            AvailableTimeslots.Add(moment);
    }

    public string AddTimeSlotList(List<Timeslot> timeList)
    {
        AvailableTimeslots.Clear();
        foreach (Timeslot slot in timeList)
        {
            AddTimeSlot(slot);
        }
        return $"Coach now has new timeslotlist";
    }

    public void RemoveTimeslot(Timeslot availableMoment)
    {
        AvailableTimeslots.Remove(availableMoment);
    }

}





