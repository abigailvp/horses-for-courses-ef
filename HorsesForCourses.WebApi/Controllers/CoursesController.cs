using Microsoft.AspNetCore.Mvc;

using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.WebApi.Repo;
using HorsesForCourses.Core.Domain;


[ApiController]
[Route("/[controller]")]
public class CourseController : ControllerBase
{
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