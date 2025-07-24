using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.WebApi.Repo;

public class CourseDTO
{
    public Id<Course> CourseId { get; set; }
    public string NameCourse { get; set; }
    public string StartDateCourse { get; set; }
    public string EndDateCourse { get; set; }

}