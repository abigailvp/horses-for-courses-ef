using Microsoft.AspNetCore.Mvc;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.Core;
using HorsesForCourses.WebApi.Repo;
using HorsesForCourses.WebApi.Factory;

namespace HorsesForCourses.WebApi.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly AllData _myMemory; //veilige methode om storage te gebruiken
        public CoursesController(AllData myMemory)
        {
            _myMemory = myMemory;
        }

        [HttpPost] // met naam en periode
        public ActionResult<CourseRequest> CreateEmptyCourse([FromBody] CourseRequest dto)
        {
            var course = new Course(dto.NameCourse, DateOnly.Parse(dto.StartDateCourse), DateOnly.Parse(dto.EndDateCourse));
            //omzetten naar DateOnly

            _myMemory.allCourses.Add(course);
            return Ok(CourseMapper.ConvertToCourseDto(course));
        }

        [HttpPost]
        [Route("{Id}/competences")] //de id gaat van in de url naar de methode
        public ActionResult<CompetentCourseRequest> AddCompetences(Guid Id, [FromBody] CompetentCourseRequest dto)
        {
            var course = _myMemory.allCourses.FirstOrDefault(c => c.CourseId.value == Id);
            if (course == null)
                return NotFound();
            course.AddCompetenceList(dto.ListOfCourseCompetences);
            return Ok(CourseMapper.ConvertToCompetentCourse(course));
        }

        [HttpPost]
        [Route("{Id}/timeslots")]
        public ActionResult<ScheduledCourseRequest> AddTimeslots(Guid Id, [FromBody] ScheduledCourseRequest dto)
        {
            var course = _myMemory.allCourses.FirstOrDefault(c => c.CourseId.value == Id);
            if (course == null)
                return NotFound();
            course.AddTimeSlotList(dto.CourseTimeslots);
            return Ok(CourseMapper.ConvertToScheduledCourse(course));
        }

        [HttpPost]
        [Route("{Id}/confirm")]
        public ActionResult<CourseRequest> ConfirmCourse(Guid Id)
        {
            var course = _myMemory.allCourses.FirstOrDefault(c => c.CourseId.value == Id);
            if (course == null)
                return NotFound();
            course.ValidateCourseBasedOnTimeslots(course);
            return Ok(CourseMapper.ConvertToCourseDto(course));
        }

        [HttpPost]
        [Route("{Id}/assign-coach")]
        public ActionResult<AssignedCourseRequest> AssignCoach(Guid Id, [FromBody] AssignedCourseRequest dto)
        {
            var course = _myMemory.allCourses.FirstOrDefault(c => c.CourseId.value == Id);
            if (course == null)
                return NotFound();
            var coach = _myMemory.allCoaches.FirstOrDefault(c => c.CoachId.value == dto.coachId);
            if (coach == null)
                return NotFound();
            course.CheckingCoach(course, coach);
            return Ok(CourseMapper.ConvertToAssignedCourse(course, coach));
        }

        [HttpGet]
        public IEnumerable<Course> GetCourses()
        => _myMemory.allCourses;


        [HttpGet]
        [Route("{Id}")]
        public ActionResult<CourseRequest> GetCourseById(Guid Id)
        {
            var course = _myMemory.allCourses.Where(c => c.CourseId.value == Id).FirstOrDefault();
            if (course == null)
                return NotFound();
            return Ok(CourseMapper.ConvertToCourseDto(course));
        }

    }
}