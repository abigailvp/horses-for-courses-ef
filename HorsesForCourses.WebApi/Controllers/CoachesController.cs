using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.WebApi.Factory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HorsesForCourses.WebApi.Controllers
{
    [ApiController]
    [Route("/[controller]")] //Coaches wordt automatisch ingevuld hier
    public class CoachesController : ControllerBase
    {
        private readonly UnitOfWork Worker;
        public CoachesController(UnitOfWork worker)
        {
            Worker = worker;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateEmptyCoach([FromBody] CoachRequest dto) //de info uit de dto wordt automatisch opgevraagd
        {
            var coach = new Coach(dto.NameCoach, dto.Email);

            await Worker.StartAsync();
            Worker.AddCoach(coach);
            await Worker.CompleteAsync();
            return Ok(coach.CoachId);
        }


        [HttpPost]
        [Route("{id}/skills")]
        public async Task<IActionResult> AddCompetencesList(int id, [FromBody] CompetentCoachRequest dto)
        {
            await Worker.StartAsync();
            var coach = await Worker.GetCoachById(id); //getting coach with same id
            if (coach == null)
                return NotFound();
            coach.AddCompetenceList(dto.ListOfSkills);
            await Worker.CompleteAsync();
            return Ok(); //geen update in repo want je hebt toegang tot coach met id
        }

        [HttpGet]
        public async Task<ActionResult<ListOfCoachesResponse>> GetCoaches()
        {
            var lijstje = await Worker.ListCoaches();
            return Ok(CoachMapper.ConvertToListOfCoaches(lijstje));
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<DetailedCoachResponse>> GetCoachById(int id)
        {
            var coach = await Worker.GetSpecificCoachById(id);
            if (coach == null)
                return NotFound();
            return Ok(CoachMapper.ConvertToDetailedCoach(coach));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteACoach(int id)
        {
            var coach = await Worker.GetCoachById(id);
            if (coach == null)
                return NotFound();
            Worker.RemoveCoach(coach);
            await Worker.CompleteAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}/skills")]
        public async Task<IActionResult> DeleteSkillsFromACoach(int id)
        {
            var coach = Worker.GetCoachById(id);
            if (coach == null)
                return NotFound();
            coach.Result.EmptyCompetenceList();
            await Worker.CompleteAsync();
            return Ok();
        }


    }

}