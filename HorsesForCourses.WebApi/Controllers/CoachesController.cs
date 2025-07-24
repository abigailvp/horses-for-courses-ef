using Microsoft.AspNetCore.Mvc;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.WebApi.Repo;

namespace HorsesForCourses.WebApi.CoachControllers
{
    [ApiController]
    [Route("/[controller]")] //Coach wordt automatisch ingevuld hier
    public class CoachesController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> CreateEmptyCoach([FromBody] CoachDTO dto) //de info uit de dto wordt automatisch opgevraagd
        {
            var coach = CoachMapper.CreateCoach(dto);
            if (coach == null)
                return BadRequest("Coach can't be added");
            AllData.allCoaches.Add(coach);
            return Ok($"Coach {coach.NameCoach} has been added");
        }


        [HttpPost]
        [Route("{coachId}/competences")]
        public ActionResult<string> AddCompetencesList(Guid coachId, [FromBody] CompetentCoachDTO dto)
        {
            var coach = AllData.allCoaches.FirstOrDefault(c => c.CoachId.value == coachId); //getting coach with same id
            if (coach == null)
                return BadRequest();
            return Ok(coach.AddCompetenceList(dto.ListOfCompetences)); //geen update in repo want je hebt toegang tot coach met id
        }
    }
}