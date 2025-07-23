using HorsesForCourses.Core;
using Microsoft.AspNetCore.Mvc;

using HorsesForCourses.Core.DomainEntities;

namespace CoachControllers
{
    [ApiController]
    [Route("/[controller]")] //Coach wordt automatisch ingevuld hier
    public class CoachController : ControllerBase
    {
        private readonly ICoachDTO _coachDTO; //field

        public CoachController(CoachDTO coachDto)//constructor
        {
            _coachDTO = coachDto;
        }

        [HttpGet]
        public IEnumerable<Coach> GetAllCoaches()
        {
            return AllData.allCoaches;
        }

        [HttpGet("{coachId:Id<Coach}")]
        [Route("coachId")]
        public Coach GetCoachById(Id<Coach> coachId)
        {
            return AllData.allCoaches.Where(c => c.CoachId == coachId).FirstOrDefault();
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult<Coach> CreateCoach([FromBody] CoachDTO request)
        => Ok(_coachDTO.CreateCoach(request));

    }
}