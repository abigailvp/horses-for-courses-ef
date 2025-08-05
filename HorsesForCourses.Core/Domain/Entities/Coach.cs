using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.Core.HorsesOnTheLoose;
using System.Dynamic;

namespace HorsesForCourses.Core.DomainEntities;

public class Coach //aggregate root
{
    public int CoachId { get; set; } //EF maakt aan
    public string NameCoach { get; set; }
    public string Email { get; set; }

    public List<Skill> ListOfCompetences { get; set; } //EF stelt in
    public List<Course> ListOfCoursesAssignedTo = new();
    public int numberOfAssignedCourses { get; set; }
    public List<Timeslot> AvailableTimeslots = new();

    public Coach() { } //EF heeft deze nodig voor migrations uit te voeren
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

        numberOfAssignedCourses = 0;
    }

    public void AddCompetence(string name)
    {
        var newCompetence = new Skill(name);
        ListOfCompetences.Add(newCompetence);
    }

    public void RemoveCompetence(string name)
    {
        if (ListOfCompetences.Select(c => c.Name).Contains(name))
            ListOfCompetences.Remove(new Skill(name));
    }

    public void AddCompetenceList(List<Skill> complist)
    {
        ListOfCompetences.Clear();
        foreach (Skill comp in complist)
        {
            ListOfCompetences.Add(comp);
        }
    }


}





