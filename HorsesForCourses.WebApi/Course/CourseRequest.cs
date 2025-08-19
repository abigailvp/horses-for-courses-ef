
using System.ComponentModel.DataAnnotations;

namespace HorsesForCourses.WebApi.Factory;

public class CourseRequest
{
    [Required]
    public string NameCourse { get; set; }

    [Required]
    public DateOnly StartDateCourse { get; set; }

    [Required]
    public DateOnly EndDateCourse { get; set; }



}
