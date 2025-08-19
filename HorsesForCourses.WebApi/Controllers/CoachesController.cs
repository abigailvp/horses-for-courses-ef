using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.WebApi.Factory;
using Microsoft.AspNetCore.Mvc;
using HorsesForCourses.Repo;
using HorsesForCourses.Paging;
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

            oneTransaction.Coaches.AddCoach(coach);
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
        public async Task<ActionResult<PagedResult<Coach>>> GetCoachesByPage(int numberOfPage, int amountOfCoaches)
        {
            var lijstje = await oneTransaction.Coaches.GetCoachPages(numberOfPage, amountOfCoaches);
            if (lijstje == null)
                return NotFound();
            return Ok(lijstje);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<DetailedCoach?>> GetCoachById(int id)
        {
            var coach = await oneTransaction.Coaches.GetSpecificCoachById(id);
            if (coach == null)
                return NotFound();
            return Ok(coach);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteACoach(int id)
        {
            var coach = await oneTransaction.Coaches.GetCoachById(id);
            if (coach == null)
                return NotFound();
            oneTransaction.Coaches.RemoveCoach(coach);
            await oneTransaction.CompleteAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}/skills")]
        public async Task<IActionResult> DeleteSkillsFromACoach(int id)
        {
            var coach = oneTransaction.Coaches.GetCoachById(id);
            if (coach == null)
                return NotFound();
            coach.Result.EmptyCompetenceList();
            await oneTransaction.CompleteAsync();
            return Ok();
        }


    }

}