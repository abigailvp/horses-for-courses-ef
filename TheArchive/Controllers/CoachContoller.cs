using Microsoft.AspNetCore.Mvc;

using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Services;
using HorsesForCourses.Core.WholeValuesAndStuff;

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
        [Route("{courseId}/Create")]
        public ActionResult<string> CreateEmptyCoach([FromBody] CoachDTO dto)
        => Ok(_coachService.CreateCoach(dto));

        [HttpPost]
        [Route("{courseId}/Assign")]
        public ActionResult<string> CreateCoach(Guid courseId, [FromBody] CoachDTO dto)
        {
            var course = AllData.allCourses.FirstOrDefault(c => c.CourseId.value == courseId); //getting coach with same id
            var result = _coachService.CreateAndAssignCoach(course, dto);

            return result.Contains("isn't") ? BadRequest(result) : Ok(result);
        }

        [HttpPost]
        [Route("{coachId}/skills")]
        public ActionResult<string> AddCompetence(Guid coachId, Competence comp [FromBody] CoachDTO dto)
        {
            var coach = AllData.allCoaches.FirstOrDefault(c => c.CoachId.value == coachId);
            var result = _coachService.AddCompetence(coach, comp);
            return Ok(result);
        }

        //         POST /coaches/{id}/skills
        //      Als administrator Wil ik competenties kunnen toevoegen of verwijderen 
        //bij een coach Zodat zijn of haar geschiktheid aangepast kan worden

        // Inkomende Dto bevat een lijst van alle skills, het domein verwijdert 
        // of voegt deze toe naar gelang of maakt de coach skill lijst leeg en repopulate deze met binnenkomende.

    }
}