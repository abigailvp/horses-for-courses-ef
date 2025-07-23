using Microsoft.AspNetCore.Mvc;
using HorsesForCourses.Core;

using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.Services;


[ApiController]
[Route("/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(CourseService courseSer)
    {
        _courseService = courseSer;
    }

    [HttpGet]
    public IEnumerable<Course> GetAllCourses()
    {
        return AllData.allCourses;
    }

    [HttpGet("{name:string}")] //name moet string zijn
    [Route("name")] //extra parameter aan url toevoegen
    public Course GetCourseByName(string name)
    {
        return AllData.allCourses.Where(c => c.NameCourse == name).FirstOrDefault();
    }

    [HttpDelete("{name:string}")]
    public ActionResult<string> DeleteCourseByName(string name)
    => Ok("deleted");


    [HttpPost]
    [Route("Create")]
    public ActionResult<string> CreateEmptyCourse([FromBody] CourseDTO dto)
    => Ok(_courseService.CreateEmptyCourse(dto));

}