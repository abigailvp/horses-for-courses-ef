using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.WebApi.Factory;

public class CompetentCourseRequest
{
    public Guid CourseId { get; set; }
    public List<Competence> ListOfCourseCompetences { get; set; }

}