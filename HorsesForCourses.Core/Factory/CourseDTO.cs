using HorsesForCourses.Core;
using HorsesForCourses.Core.DomainEntities;

public class CourseDTO : ICourseDTO
{
    public string NameCourse { get; set; }
    public string StartDateCourse { get; set; }
    public string EndDateCourse { get; set; }
    // public int DurationCourse { get; set; }
    // public Coach CoachForCourse { get; set; }

    public string CreateCourse(CourseDTO dto)
    {
        Course course = new(dto.NameCourse, DateOnly.Parse(dto.EndDateCourse), DateOnly.Parse(dto.StartDateCourse));
        AllData.allCourses.Add(course);
        return $"Course has the name {course.NameCourse}. It starts at {course.EndDateCourse} and ends at {course.EndDateCourse}.";
    }
}