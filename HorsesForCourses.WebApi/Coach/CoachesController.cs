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
        public ActionResult<int> CreateEmptyCoach([FromBody] CoachRequest dto) //de info uit de dto wordt automatisch opgevraagd
        {
            var coach = new Coach(dto.NameCoach, dto.Email);

            _myMemory.allCoaches.Add(coach);
            return Ok(coach.CoachId);
        }


        [HttpPost]
        [Route("{id}/skills")]
        public IActionResult AddCompetencesList(int id, [FromBody] CompetentCoachRequest dto)
        {
            var coach = _myMemory.allCoaches.FirstOrDefault(c => c.CoachId == id); //getting coach with same id
            if (coach == null)
                return NotFound();
            coach.AddCompetenceList(dto.ListOfSkills);
            return Ok(); //geen update in repo want je hebt toegang tot coach met id
        }

        // [HttpPost]
        // [Route("{Id}/timeslots")]
        // public ActionResult<ScheduledCoachRequest> AddTimeslots(int Id, [FromBody] ScheduledCoachRequest dto)
        // {
        //     var coach = _myMemory.allCoaches.FirstOrDefault(c => c.CoachId == Id);
        //     if (coach == null)
        //         return NotFound();
        //     coach.AddTimeSlotList(dto.CoachTimeslots);
        //     return Ok(CoachMapper.ConvertToScheduledCoach(coach));
        // }

        [HttpGet]
        public ActionResult<ListOfCoachesResponse> GetCoaches()
        {
            var lijstje = _myMemory.allCoaches;
            return Ok(CoachMapper.ConvertToListOfCoaches(lijstje));
        }


        [HttpGet]
        [Route("{id}")]
        public ActionResult<DetailedCoachResponse> GetCoachById(int id)
        {
            var coach = _myMemory.allCoaches.Where(c => c.CoachId == id).FirstOrDefault();
            if (coach == null)
                return NotFound();
            return Ok(CoachMapper.ConvertToDetailedCoach(coach));
        }


    }

}