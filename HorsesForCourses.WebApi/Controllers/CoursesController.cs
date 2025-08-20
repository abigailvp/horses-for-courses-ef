using Microsoft.AspNetCore.Mvc;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.WebApi.Factory;
using HorsesForCourses.Repo;
using HorsesForCourses.Paging;
using static HorsesForCourses.Repo.CoursesRepo;

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
            var course = new Course(dto.NameCourse, dto.StartDateCourse, dto.EndDateCourse);

            await transaction.Courses.AddCourse(course);
            await transaction.CompleteAsync();
            return Ok(course.CourseId);
        }

        [HttpPost]
        [Route("{Id}/skills")] //de id gaat van in de url naar de methode
        public async Task<IActionResult> AddCompetences(int Id, [FromBody] CompetentCourseRequest dto)
        {
            var course = await transaction.Courses.GetCourseById(Id);
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
            var course = await transaction.Courses.GetCourseById(Id);
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
            var course = await transaction.Courses.GetCourseById(Id);
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
            var course = await transaction.Courses.GetCourseById(Id);
            if (course == null)
                return NotFound();
            var coach = await transaction.Coaches.GetCoachById(dto.coachId);
            if (coach == null)
                return NotFound();
            course.AddingCoach(course, coach);
            await transaction.CompleteAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CourseResponse>>> GetAllCourses()
        {
            var allCourses = await transaction.Courses.ListCompactCourses();
            if (allCourses == null)
                return NotFound();
            await transaction.CompleteAsync();
            return Ok(allCourses);
        }

        [HttpGet]
        [Route("page{numberOfPage}/{amountOfCourses}")]
        public async Task<ActionResult<PagedResult<CourseResponse>>> GetCoursesByPage(int numberOfPage, int amountOfCourses)
        {
            var lijstje = await transaction.Courses.GetCoursePages(numberOfPage, amountOfCourses);
            if (lijstje == null)
                return NotFound();
            return Ok(lijstje);
        }



        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult<DetailedCourse?>> GetCourseById(int Id)
        {
            var course = await transaction.Courses.GetSpecificCourseById(Id);
            if (course == null)
                return NotFound();
            await transaction.CompleteAsync();
            return Ok(course);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteACourse(int id)
        {
            var course = await transaction.Courses.GetCourseById(id);
            if (course == null)
                return NotFound();
            transaction.Courses.RemoveCourse(course);
            await transaction.CompleteAsync();
            return Ok();

        }

        [HttpDelete]
        [Route("noDates/{id}")]
        public async Task<IActionResult> DeleteACourseWithoutDates(int id)
        {
            transaction.Courses.DeleteCourseWithoutDates(id);
            await transaction.CompleteAsync();
            return Ok();
        }

    }
}