using HorsesForCourses.Core.DomainEntities;

namespace HorsesForCourses.Core.WholeValuesAndStuff;

public class CompetentCourseDTO
{
    public Id<Course> CourseId { get; set; }
    // public string NameCourse { get; set; }
    // public string StartDateCourse { get; set; }
    // public string EndDateCourse { get; set; }

    public List<Competence> ListOfCourseCompetences { get; set; }

}