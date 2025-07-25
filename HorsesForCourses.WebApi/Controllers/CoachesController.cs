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
            return Ok($"Coach {coach.NameCoach} has been added and has ID {coach.CoachId.value}");
        }


        [HttpPost]
        [Route("{coachId}/competences")]
        public ActionResult<string> AddCompetencesList(Guid coachId, [FromBody] CompetentCoachRequest dto)
        {
            var coach = _myMemory.allCoaches.FirstOrDefault(c => c.CoachId.value == coachId); //getting coach with same id
            if (coach == null)
                return BadRequest("Id doesn't match with a coach.");
            return Ok(coach.AddCompetenceList(dto.ListOfCompetences)); //geen update in repo want je hebt toegang tot coach met id
        }
    }
}