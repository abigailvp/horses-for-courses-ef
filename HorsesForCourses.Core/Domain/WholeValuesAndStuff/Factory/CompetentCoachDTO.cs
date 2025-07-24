namespace HorsesForCourses.Core.WholeValuesAndStuff;

public class CompetentCoachDTO
{
    public Guid CoachId { get; set; }
    public string NameCoach { get; set; }
    public string Email { get; set; }

    public List<Competence> ListOfCompetences { get; set; }

}