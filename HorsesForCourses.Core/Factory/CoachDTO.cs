using HorsesForCourses.Core;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

public class CoachDTO
{
    public Guid CoachId { get; set; }
    public string NameCoach { get; set; }
    public string Email { get; set; }

    public List<Competence> ListOfCompetences { get; set; }
    public Dictionary<DateOnly, List<Timeslot>> AvailableTimeslots { get; set; }

}