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
            return Ok(_myMemory.ConvertToCourse(course));
        }

        [HttpPost]
        [Route("{courseId}/competences")] //de id gaat van in de url naar de methode
        public ActionResult<CompetentCourseRequest> AddCompetences(Guid courseId, [FromBody] CompetentCourseRequest dto)
        {
            var course = _myMemory.allCourses.FirstOrDefault(c => c.CourseId.value == courseId);
            if (course == null)
                return NotFound();
            course.AddCompetenceList(dto.ListOfCourseCompetences);
            return Ok(_myMemory.ConvertToCompetentCourse(course));
        }

        [HttpPost]
        [Route("{courseId}/timeslots")]
        public ActionResult<ScheduledCourseRequest> AddTimeslots(Guid courseId, [FromBody] ScheduledCourseRequest dto)
        {
            var course = _myMemory.allCourses.FirstOrDefault(c => c.CourseId.value == courseId);
            if (course == null)
                return NotFound();
            course.AddTimeSlotList(dto.CourseTimeslots);
            return Ok(_myMemory.ConvertToScheduledCourse(course));
        }

        [HttpPost]
        [Route("{courseId}/confirm")]
        public ActionResult<CourseRequest> ConfirmCourse(Guid courseId)
        {
            var course = _myMemory.allCourses.FirstOrDefault(c => c.CourseId.value == courseId);
            if (course == null)
                return NotFound();
            Availability.ValidateCourseBasedOnTimeslots(course);
            return Ok(_myMemory.ConvertToCourse(course));
        }

        [HttpPost]
        [Route("{courseId}/assign-coach")]
        public ActionResult<AssignedCourseRequest> AssignCoach(Guid courseId, [FromBody] AssignedCourseRequest dto)
        {
            var course = _myMemory.allCourses.FirstOrDefault(c => c.CourseId.value == courseId);
            if (course == null)
                return BadRequest($"Course wasn't found with {courseId}");
            var coach = _myMemory.allCoaches.FirstOrDefault(c => c.CoachId.value == dto.coachId);
            if (coach == null)
                return BadRequest($"Coach wasn't found with {dto.coachId}");
            Availability.CheckingCoach(course, coach);
            return Ok(_myMemory.ConvertToAssignedCourse(course, coach));
        }

        [HttpGet]
        public IEnumerable<Course> GetCourses()
        => _myMemory.allCourses;


        [HttpGet]
        [Route("{courseId}")]
        public ActionResult<string> GetCourseById(Guid courseId)
        {
            var course = _myMemory.allCourses.Where(c => c.CourseId.value == courseId).FirstOrDefault();
            if (course == null)
                return NotFound();
            return Ok($"Course has the name {course.NameCourse}. It starts at {course.StartDateCourse} and ends at {course.EndDateCourse}.");
        }

    }
}