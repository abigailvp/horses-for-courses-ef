using HorsesForCourses.Core;

namespace HorsesForCourses.Course;

public class Coach //aggregate root
{
    public Id<Coach> CoachId { get; }
    public string NameCoach { get; }
    public string Email { get; }

    private readonly List<Competence> ListOfCompetences = new List<Competence>();

    public record NumberOfWeek(int numberOfWeek);
    public readonly Dictionary<Weekdays, Timeslot> AvailableTimeslots = new Dictionary<Weekdays, Timeslot>();

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

    public void AddTimeSlot(Timeslot availableMoment)
    {
        AvailableTimeslots.Add(availableMoment.WeekdayTimeslot, availableMoment);
    }

    public void RemoveTimeslot(Timeslot availableMoment)
    {
        AvailableTimeslots.Remove(availableMoment.WeekdayTimeslot);
    }

    // public bool CheckAvailability()
    // {
    //     if(AvailableTimeslots.Contains())
    //     return true;

    //     if ()
    //         return false;
    // }
}



