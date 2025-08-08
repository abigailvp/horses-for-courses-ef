using Microsoft.AspNetCore.Mvc;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.WebApi.Factory;

namespace HorsesForCourses.WebApi.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly IUnitOfWork transaction;
        public CoursesController(IUnitOfWork trans)
        {
            transaction = trans;
        }

        [HttpPost] // met naam en periode
        public async Task<ActionResult<int>> CreateEmptyCourse([FromBody] CourseRequest dto)
        {
            var course = new Course(dto.NameCourse, DateOnly.Parse(dto.StartDateCourse), DateOnly.Parse(dto.EndDateCourse));
            //omzetten naar DateOnly
            await transaction.Objects.AddCourse(course);
            await transaction.CompleteAsync();
            return Ok(course.CourseId);
        }

        [HttpPost]
        [Route("{Id}/skills")] //de id gaat van in de url naar de methode
        public async Task<IActionResult> AddCompetences(int Id, [FromBody] CompetentCourseRequest dto)
        {
            var course = await transaction.Objects.GetCourseById(Id);
            if (course == null)
                return NotFound();
            course.AddCompetenceList(dto.ListOfCourseCompetences);
            await transaction.CompleteAsync();
            return Ok();
        }

        [HttpPost]
        [Route("{Id}/timeslots")]
        public async Task<IActionResult> AddTimeslots(int Id, [FromBody] ScheduledCourseRequest dto) //invoer van lijstdag moet format "YYYY/MM/DD" hebben
        {
            var course = await transaction.Objects.GetCourseById(Id);
            if (course == null)
                return NotFound();
            course.AddTimeSlotList(CourseMapper.ConvertToDomainList(dto.CourseTimeslots));
            await transaction.CompleteAsync();
            return Ok();
        }

        [HttpPost]
        [Route("{Id}/confirm")]
        public async Task<IActionResult> ConfirmCourse(int Id)
        {
            var course = await transaction.Objects.GetCourseById(Id);
            if (course == null)
                return NotFound();
            course.ValidateCourseBasedOnTimeslots(course);
            await transaction.CompleteAsync();
            return Ok();
        }

        [HttpPost]
        [Route("{Id}/assign-coach")]
        public async Task<IActionResult> AssignCoach(int Id, [FromBody] AssignedCourseRequest dto)
        {
            var course = await transaction.Objects.GetCourseById(Id);
            if (course == null)
                return NotFound();
            var coach = await transaction.Objects.GetCoachById(Id);
            if (coach == null)
                return NotFound();
            course.AddingCoach(course, coach);
            await transaction.CompleteAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<AllCoursesResponse>> GetCourses()
        {
            var allCourses = await transaction.Objects.ListCourses();
            if (allCourses == null)
                return NotFound();
            await transaction.CompleteAsync();
            return Ok(CourseMapper.ConvertToListCourses(allCourses));
        }



        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult<DetailedCourseResponse>> GetCourseById(int Id)
        {
            var course = await transaction.Objects.GetSpecificCourseById(Id);
            if (course == null)
                return NotFound();
            await transaction.CompleteAsync();
            return Ok(CourseMapper.ConvertToDetailedCourse(course));
        }


    }
}