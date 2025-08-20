using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Paging;
using HorsesForCourses.Repo;
using HorsesForCourses.WebApi.Factory;
using Microsoft.AspNetCore.Mvc;
using static HorsesForCourses.Repo.CoachesRepo;

namespace HorsesForCourses.WebApi.Controllers
{
    [ApiController]
    [Route("/[controller]")] //Coaches wordt automatisch ingevuld hier
    public class CoachesController : ControllerBase
    {
        private readonly IUnitOfWork oneTransaction;
        public CoachesController(IUnitOfWork onetransaction)
        {
            oneTransaction = onetransaction;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateEmptyCoach([FromBody] CoachRequest dto) //de info uit de dto wordt automatisch opgevraagd
        {
            var coach = new Coach(dto.NameCoach, dto.Email);

            await oneTransaction.Coaches.AddCoach(coach);
            await oneTransaction.CompleteAsync();
            return Ok(coach.CoachId);
        }


        [HttpPost]
        [Route("{id}/skills")]
        public async Task<IActionResult> AddCompetencesList(int id, [FromBody] CompetentCoachRequest dto)
        {
            var coach = await oneTransaction.Coaches.GetCoachById(id); //getting coach with same id
            if (coach == null)
                return NotFound();
            coach.AddCompetenceList(dto.ListOfSkills);
            await oneTransaction.CompleteAsync();
            return Ok(); //geen update in repo want je hebt toegang tot coach met id
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CoachResponse>>> GetAllCoaches()
        {
            var lijstje = await oneTransaction.Coaches.ListCoaches();
            if (lijstje == null)
                return NotFound();
            return Ok(lijstje);
        }

        [HttpGet]
        [Route("pages")]
        public async Task<ActionResult<IReadOnlyList<CoachResponse>>> GetCoachesByPage([FromQuery] int numberOfPage, [FromQuery] int amountOfCoaches)
        {
            var lijstje = await oneTransaction.Coaches.GetCoachPages(numberOfPage, amountOfCoaches);
            if (lijstje == null)
                return NotFound();
            return Ok(lijstje.Items);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<DetailedCoach?>> GetCoachById(int id)
        {
            var coachdetails = await oneTransaction.Coaches.GetSpecificCoachById(id);
            if (coachdetails == null)
                return NotFound();
            return Ok(coachdetails);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteACoach(int id)
        {
            var numberOfDeletedRecords = await oneTransaction.Coaches.RemoveCoach(id);
            if (numberOfDeletedRecords == 0)
                return NotFound();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}/skills")]
        public async Task<IActionResult> DeleteSkillsFromACoach(int id) //enkel coachobject aanpassen want skills staan als ownsmany ingesteld
        {
            var coach = oneTransaction.Coaches.GetCoachById(id).Result;
            if (coach == null)
                return NotFound();
            coach.EmptyCompetenceList();
            await oneTransaction.CompleteAsync();
            return Ok();
        }
    }
}