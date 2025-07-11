using System.Security.Cryptography.X509Certificates;
using HorsesForCourses.Core;

namespace HorsesForCourses.Core;

public class Coach //aggregate root
{
    public Id<Coach> CoachId { get; }
    public string NameCoach { get; }
    public string Email { get; }

    private readonly List<Competence> ListOfCompetences = new List<Competence>();

    public record NumberOfWeek(int numberOfWeek);
    public readonly Dictionary<DateOnly, Timeslot> AvailableTimeslots = new Dictionary<DateOnly, Timeslot>();

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
        AvailableTimeslots.Add(availableMoment.DayTimeslot, availableMoment);
    }

    public void RemoveTimeslot(Timeslot availableMoment)
    {
        AvailableTimeslots.Remove(availableMoment.DayTimeslot);
    }

    // public bool CheckAvailability(Course course)
    // {
    //     var courseTimeslots = course.CourseTimeslots;
    //     var onlyMatchingDates = courseTimeslots.Join(AvailableTimeslots,//list for coach
    //     c => c.Key,
    //     a => a.Key,
    //     (c, a) => new { courseTimeslot = c.Value, coachTimeslot = a.Value });

    //     //genoeg pairs????


    //     foreach (var pair in onlyMatchingDates)
    //     {
    //         var courseSlot = pair.courseTimeslot;
    //         var coachSlot = pair.coachTimeslot;

    //         if (courseSlot.BeginTimeslot <= coachSlot.BeginTimeslot &&
    //             courseSlot.EndTimeslot <= coachSlot.EndTimeslot)
    //             return true;
    //         else
    //             return false;
    //     }
    // }
}





