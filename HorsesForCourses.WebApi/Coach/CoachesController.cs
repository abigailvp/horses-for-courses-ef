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
        private readonly AppDbContext Context;
        public CoachesController(AppDbContext ctx)
        {
            Context = ctx;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateEmptyCoach([FromBody] CoachRequest dto) //de info uit de dto wordt automatisch opgevraagd
        {
            var coach = new Coach(dto.NameCoach, dto.Email);

            await Context.Database.EnsureCreatedAsync();
            Context.Coaches.Add(coach);
            await Context.SaveChangesAsync();
            return Ok(coach.CoachId);
        }


        [HttpPost]
        [Route("{id}/skills")]
        public async Task<IActionResult> AddCompetencesList(int id, [FromBody] CompetentCoachRequest dto)
        {
            var coach = await Context.Coaches
            .FirstOrDefaultAsync(c => c.CoachId == id); //getting coach with same id
            if (coach == null)
                return NotFound();
            coach.AddCompetenceList(dto.ListOfSkills);
            await Context.SaveChangesAsync();
            return Ok(); //geen update in repo want je hebt toegang tot coach met id
        }

        [HttpGet]
        public async Task<ActionResult<ListOfCoachesResponse>> GetCoaches()
        {
            var lijstje = await Context.Coaches.ToListAsync();
            return Ok(CoachMapper.ConvertToListOfCoaches(lijstje));
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<DetailedCoachResponse>> GetCoachById(int id)
        {
            var coach = await Context.Coaches
            .Include(c => c.ListOfCompetences)
            .Include(c => c.ListOfCoursesAssignedTo)
            .FirstOrDefaultAsync(c => c.CoachId == id);
            if (coach == null)
                return NotFound();
            return Ok(CoachMapper.ConvertToDetailedCoach(coach));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteACoach(int id)
        {
            var coach = await Context.Coaches.FirstOrDefaultAsync(c => c.CoachId == id);
            if (coach == null)
                return NotFound();
            Context.Coaches.Remove(coach);
            await Context.SaveChangesAsync();
            return Ok();
        }


    }

}