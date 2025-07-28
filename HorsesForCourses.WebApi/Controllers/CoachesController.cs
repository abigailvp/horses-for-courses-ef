using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.WebApi.Factory;
using HorsesForCourses.WebApi.Repo;
using Microsoft.AspNetCore.Mvc;

namespace HorsesForCourses.WebApi.Controllers
{
    [ApiController]
    [Route("/[controller]")] //Coach wordt automatisch ingevuld hier
    public class CoachesController : ControllerBase
    {
        private readonly AllData _myMemory; //veilige methode om storage te gebruiken
        public CoachesController(AllData myMemory)
        {
            _myMemory = myMemory;
        }

        [HttpPost]
        public ActionResult<string> CreateEmptyCoach([FromBody] CoachRequest dto) //de info uit de dto wordt automatisch opgevraagd
        {
            var coach = new Coach(dto.NameCoach, dto.Email)
            { CoachId = new Id<Coach>(dto.CoachId) };

            _myMemory.allCoaches.Add(coach);
            return Ok(_myMemory.ConvertToCoach(coach));
        }


        [HttpPost]
        [Route("{coachId}/competences")]
        public ActionResult<CompetentCoachRequest> AddCompetencesList(Guid coachId, [FromBody] CompetentCoachRequest dto)
        {
            var coach = _myMemory.allCoaches.FirstOrDefault(c => c.CoachId.value == coachId); //getting coach with same id
            if (coach == null)
                return NotFound();
            coach.AddCompetenceList(dto.ListOfCompetences);
            return Ok(_myMemory.ConvertToCompetentCoach(coach)); //geen update in repo want je hebt toegang tot coach met id
        }

        [HttpPost]
        [Route("{coachId}/timeslots")]
        public ActionResult<ScheduledCoachRequest> AddTimeslots(Guid coachId, [FromBody] ScheduledCoachRequest dto)
        {
            var coach = _myMemory.allCoaches.FirstOrDefault(c => c.CoachId.value == coachId);
            if (coach == null)
                return NotFound();
            coach.AddTimeSlotList(dto.CoachTimeslots);
            return Ok(_myMemory.ConvertToScheduledCoach(coach));
        }

        [HttpGet]
        public IEnumerable<Coach> GetCoaches()
        => _myMemory.allCoaches;


        [HttpGet]
        [Route("{coachId}")]
        public ActionResult<CoachRequest> GetCoachById(Guid coachId)
        {
            var coach = _myMemory.allCoaches.Where(c => c.CoachId.value == coachId).FirstOrDefault();
            if (coach == null)
                return NotFound();
            return Ok(_myMemory.ConvertToCoach(coach));
        }


    }

}