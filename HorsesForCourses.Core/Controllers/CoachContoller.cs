using HorsesForCourses.Core;
using Microsoft.AspNetCore.Mvc;

using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.Services;

namespace CoachControllers
{
    [ApiController]
    [Route("/[controller]")] //Coach wordt automatisch ingevuld hier
    public class CoachController : ControllerBase
    {
        private readonly ICoachService _coachService; //field

        public CoachController(CoachService coachser)//constructor
        {
            _coachService = coachser;
        }

        [HttpGet]
        public IEnumerable<Coach> GetAllCoaches()
        {
            return AllData.allCoaches;
        }

        [HttpGet("{coachId:Id<Coach}")]
        [Route("coachId")]
        public Coach GetCoachById(Guid coachId)
        {
            return AllData.allCoaches.Where(c => c.CoachId.value == coachId).FirstOrDefault();
        }

        [HttpPost]
        [Route("{courseId}/CreateCoach")]
        public ActionResult<string> CreateCoach(Guid courseId, [FromBody] CoachDTO dto)
        {
            var course = AllData.allCourses.FirstOrDefault(c => c.CourseId.value == courseId); //getting coach with same id
            var result = _coachService.CreateAndAssignCoach(course, dto);

            return result.Contains("isn't") ? BadRequest(result) : Ok(result);
        }

    }
}