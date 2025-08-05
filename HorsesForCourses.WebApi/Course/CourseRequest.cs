
using System.ComponentModel.DataAnnotations;

namespace HorsesForCourses.WebApi.Factory;

public class CourseRequest
{
    [Required]
    public string NameCourse { get; set; }

    [Required]
    public string StartDateCourse { get; set; }

    [Required]
    public string EndDateCourse { get; set; }



}
