using Microsoft.AspNetCore.Mvc;
using HorsesForCourses.Core;

using HorsesForCourses.Core.DomainEntities;

[ApiController]
[Route("/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseDTO _courseDTO;

    public CourseController(CourseDTO coursedto)
    {
        _courseDTO = coursedto;
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
    public ActionResult<string> CreateEmptyCourse([FromBody] CourseDTO request)
    => Ok(_courseDTO.CreateCourse(request));

}