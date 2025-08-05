using Microsoft.AspNetCore.Mvc;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.WebApi.Factory;
using Microsoft.EntityFrameworkCore;

namespace HorsesForCourses.WebApi.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext Context; //veilige methode om storage te gebruiken
        public CoursesController(AppDbContext ctx)
        {
            Context = ctx;
        }

        [HttpPost] // met naam en periode
        public async Task<ActionResult<int>> CreateEmptyCourse([FromBody] CourseRequest dto)
        {
            var course = new Course(dto.NameCourse, DateOnly.Parse(dto.StartDateCourse), DateOnly.Parse(dto.EndDateCourse));
            //omzetten naar DateOnly

            await Context.Database.EnsureCreatedAsync();
            Context.Courses.Add(course);
            await Context.SaveChangesAsync();
            return Ok(course.CourseId);
        }

        [HttpPost]
        [Route("{Id}/skills")] //de id gaat van in de url naar de methode
        public async Task<IActionResult> AddCompetences(int Id, [FromBody] CompetentCourseRequest dto)
        {
            var course = await Context.Courses.FirstOrDefaultAsync(c => c.CourseId == Id);
            if (course == null)
                return NotFound();
            course.AddCompetenceList(dto.ListOfCourseCompetences);
            await Context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("{Id}/timeslots")]
        public async Task<IActionResult> AddTimeslots(int Id, [FromBody] ScheduledCourseRequest dto) //invoer van lijstdag moet format "YYYY/MM/DD" hebben
        {
            var course = await Context.Courses.FirstOrDefaultAsync(c => c.CourseId == Id);
            if (course == null)
                return NotFound();
            course.AddTimeSlotList(CourseMapper.ConvertToDomainList(dto.CourseTimeslots));
            await Context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("{Id}/confirm")]
        public async Task<IActionResult> ConfirmCourse(int Id)
        {
            var course = await Context.Courses.FirstOrDefaultAsync(c => c.CourseId == Id);
            if (course == null)
                return NotFound();
            course.ValidateCourseBasedOnTimeslots(course);
            await Context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("{Id}/assign-coach")]
        public async Task<IActionResult> AssignCoach(int Id, [FromBody] AssignedCourseRequest dto)
        {
            var course = await Context.Courses.FirstOrDefaultAsync(c => c.CourseId == Id);
            if (course == null)
                return NotFound();
            var coach = await Context.Coaches.FirstOrDefaultAsync(c => c.CoachId == dto.coachId);
            if (coach == null)
                return NotFound();
            course.AddingCoach(course, coach);
            await Context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<AllCoursesResponse>> GetCourses()
        {
            var allCourses = await Context.Courses.ToListAsync();
            await Context.SaveChangesAsync();
            return CourseMapper.ConvertToListCourses(allCourses);
        }



        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult<DetailedCourseResponse>> GetCourseById(int Id)
        {
            var course = await Context.Courses
            .Include(c => c.NameCourse)
            .Include(c => c.StartDateCourse)
            .Include(c => c.EndDateCourse)
            .Include(c => c.ListOfCourseSkills)
            .Include(c => c.CourseTimeslots)
            .Include(c => c.CoachForCourse)
            .FirstOrDefaultAsync(c => c.CourseId == Id);
            if (course == null)
                return NotFound();
            return Ok(CourseMapper.ConvertToDetailedCourse(course));
        }


    }
}