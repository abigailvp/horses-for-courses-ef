using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.Core.HorsesOnTheLoose;

namespace HorsesForCourses.Core.DomainEntities;

public class Coach //aggregate root
{
    public int CoachId { get; set; }
    public string NameCoach { get; set; }
    public string Email { get; set; }

    public List<Skill> ListOfCompetences = new();
    public List<Course> ListOfCoursesAssignedTo = new();
    public int numberOfAssignedCourses { get; set; }
    public List<Timeslot> AvailableTimeslots = new();


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
        CoachId = new int(); //pas aanmaken als je een coach aanmaakt

        numberOfAssignedCourses = 0;
    }

    public void AddCompetence(string name)
    {
        var newCompetence = new Skill(name);
        ListOfCompetences.Add(newCompetence);
    }

    public void RemoveCompetence(Skill competence)
    {
        ListOfCompetences.Remove(competence);
    }

    public string AddCompetenceList(List<Skill> complist)
    {
        ListOfCompetences.Clear();
        foreach (Skill comp in complist)
        {
            ListOfCompetences.Add(comp);
        }
        return $"Coach has new competences list";
    }

    public void AddTimeSlot(Timeslot moment)
    {
        IEnumerable<Timeslot> slots = AvailableTimeslots.Where(c => c.DateTimeslot == moment.DateTimeslot);
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





