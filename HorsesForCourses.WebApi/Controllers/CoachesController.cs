using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.WebApi.Factory;
using Microsoft.AspNetCore.Mvc;

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

            oneTransaction.Objects.AddCoach(coach);
            await oneTransaction.CompleteAsync();
            return Ok(coach.CoachId);
        }


        [HttpPost]
        [Route("{id}/skills")]
        public async Task<IActionResult> AddCompetencesList(int id, [FromBody] CompetentCoachRequest dto)
        {
            var coach = await oneTransaction.Objects.GetCoachById(id); //getting coach with same id
            if (coach == null)
                return NotFound();
            coach.AddCompetenceList(dto.ListOfSkills);
            await oneTransaction.CompleteAsync();
            return Ok(); //geen update in repo want je hebt toegang tot coach met id
        }

        [HttpGet]
        public async Task<ActionResult<ListOfCoachesResponse>> GetCoaches()
        {
            var lijstje = await oneTransaction.Objects.ListCoaches();
            return Ok(CoachResponses.ConvertToListOfCoaches(lijstje));
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<DetailedCoachResponse>> GetCoachById(int id)
        {
            var coach = await oneTransaction.Objects.GetSpecificCoachById(id);
            if (coach == null)
                return NotFound();
            return Ok(CoachResponses.ConvertToDetailedCoach(coach));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteACoach(int id)
        {
            var coach = await oneTransaction.Objects.GetCoachById(id);
            if (coach == null)
                return NotFound();
            oneTransaction.Objects.RemoveCoach(coach);
            await oneTransaction.CompleteAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}/skills")]
        public async Task<IActionResult> DeleteSkillsFromACoach(int id)
        {
            var coach = oneTransaction.Objects.GetCoachById(id);
            if (coach == null)
                return NotFound();
            coach.Result.EmptyCompetenceList();
            await oneTransaction.CompleteAsync();
            return Ok();
        }


    }

}