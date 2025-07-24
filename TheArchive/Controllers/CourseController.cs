using Microsoft.AspNetCore.Mvc;

using HorsesForCourses.Core.DomainEntities;
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

    [HttpGet("{idcourse:Guid}")] //name moet string zijn
    [Route("name")] //extra parameter aan url toevoegen
    public Course GetCourseById(Guid idcourse)
    {
        return AllData.allCourses.Where(c => c.CourseId.value == idcourse).FirstOrDefault();
    }

    [HttpDelete("{name:string}")]
    public ActionResult<string> DeleteCourseByName(string name)
    => Ok("deleted");


    [HttpPost]
    [Route("Create")]
    public ActionResult<string> CreateEmptyCourse([FromBody] CourseDTO dto)
    => Ok(_courseService.CreateEmptyCourse(dto));

    [HttpPost]
    [Route("Assign")]
    public ActionResult<string> AssignCourse(Guid idCoach, [FromBody] CourseDTO dto)
    {
        var coach = AllData.allCoaches.Where(c => c.CoachId.value == idCoach).FirstOrDefault();
        return Ok(_courseService.CreateAndAssignCourse(coach, dto));
    }

}