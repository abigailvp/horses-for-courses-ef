using HorsesForCourses.Core.DomainEntities;

namespace HorsesForCourses.Core.WholeValuesAndStuff;

public class CourseDTO
{
    public Id<Course> CourseId { get; set; }
    public string NameCourse { get; set; }
    public string StartDateCourse { get; set; }
    public string EndDateCourse { get; set; }

}

public class CourseMapper
{
    public static Course CreateEmptyCourse(CourseDTO dto)
    {
        return new Course(dto.NameCourse, DateOnly.Parse(dto.StartDateCourse), DateOnly.Parse(dto.EndDateCourse));
        //omzetten naar DateOnly
    }
}