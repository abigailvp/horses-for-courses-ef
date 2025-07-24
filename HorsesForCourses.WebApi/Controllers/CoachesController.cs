using Microsoft.AspNetCore.Mvc;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.WebApi.Repo;

namespace CoachControllers
{
    [ApiController]
    [Route("/[controller]")] //Coach wordt automatisch ingevuld hier
    public class CoachesController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> CreateEmptyCoach([FromBody] CoachDTO dto) //de info uit de dto wordt automatisch opgevraagd
        => Ok(AllData.CreateCoach(dto));


        [HttpPost]
        [Route("{coachId}/competences")]
        public ActionResult<string> AddCompetencesList(Guid coachId, [FromBody] CompetentCoachDTO dto)
        {
            var coach = AllData.allCoaches.FirstOrDefault(c => c.CoachId.value == coachId); //getting coach with same id
            return Ok(coach.AddCompetenceList(dto.ListOfCompetences)); //geen update in repo want je hebt toegang tot coach met id
        }

    }
}