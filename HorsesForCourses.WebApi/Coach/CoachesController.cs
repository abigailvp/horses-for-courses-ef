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
        public ActionResult<CoachRequest> CreateEmptyCoach([FromBody] CoachRequest dto) //de info uit de dto wordt automatisch opgevraagd
        {
            var coach = new Coach(dto.NameCoach, dto.Email)
            { CoachId = new Id<Coach>(dto.CoachId) };

            _myMemory.allCoaches.Add(coach);
            return Ok(CoachMapper.ConvertToCoachDto(coach));
        }


        [HttpPost]
        [Route("{Id}/competences")]
        public ActionResult<CompetentCoachRequest> AddCompetencesList(Guid Id, [FromBody] CompetentCoachRequest dto)
        {
            var coach = _myMemory.allCoaches.FirstOrDefault(c => c.CoachId.value == Id); //getting coach with same id
            if (coach == null)
                return NotFound();
            coach.AddCompetenceList(dto.ListOfCompetences);
            return Ok(CoachMapper.ConvertToCompetentCoach(coach)); //geen update in repo want je hebt toegang tot coach met id
        }

        [HttpPost]
        [Route("{Id}/timeslots")]
        public ActionResult<ScheduledCoachRequest> AddTimeslots(Guid Id, [FromBody] ScheduledCoachRequest dto)
        {
            var coach = _myMemory.allCoaches.FirstOrDefault(c => c.CoachId.value == Id);
            if (coach == null)
                return NotFound();
            coach.AddTimeSlotList(dto.CoachTimeslots);
            return Ok(CoachMapper.ConvertToScheduledCoach(coach));
        }

        [HttpGet]
        public IEnumerable<Coach> GetCoaches()
        => _myMemory.allCoaches;


        [HttpGet]
        [Route("{Id}")]
        public ActionResult<CoachRequest> GetCoachById(Guid Id)
        {
            var coach = _myMemory.allCoaches.Where(c => c.CoachId.value == Id).FirstOrDefault();
            if (coach == null)
                return NotFound();
            return Ok(CoachMapper.ConvertToCoachDto(coach));
        }


    }

}