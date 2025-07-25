using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.WebApi.Factory;

public class CompetentCoachRequest
{
    public Guid CoachId { get; set; }
    public List<Competence> ListOfCompetences { get; set; }
}