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
        public ActionResult<string> CreateEmptyCourse([FromBody] CourseRequest dto)
        {
            var course = new Course(dto.NameCourse, DateOnly.Parse(dto.StartDateCourse), DateOnly.Parse(dto.EndDateCourse));
            //omzetten naar DateOnly

            _myMemory.allCourses.Add(course);
            return Ok($"Course {course.NameCourse} has been added with ID {course.CourseId}");
        }

        [HttpPost]
        [Route("{courseId}/competences")] //de id gaat van in de url naar de methode
        public ActionResult<string> AddCompetences(Guid courseId, [FromBody] CompetentCourseRequest dto)
        {
            var course = _myMemory.allCourses.FirstOrDefault(c => c.CourseId.value == courseId);
            if (course == null)
                return NotFound();
            return Ok(course.AddCompetenceList(dto.ListOfCourseCompetences));
        }

        [HttpPost]
        [Route("{courseId}/timeslots")]
        public ActionResult<string> AddTimeslots(Guid courseId, [FromBody] ScheduledCourseRequest dto)
        {
            var course = _myMemory.allCourses.FirstOrDefault(c => c.CourseId.value == courseId);
            if (course == null)
                return NotFound();
            return Ok(course.AddTimeSlotList(dto.CourseTimeslots));
        }

        [HttpPost]
        [Route("{courseId}/confirm")]
        public ActionResult<StatusCourse> ConfirmCourse(Guid courseId)
        {
            var course = _myMemory.allCourses.FirstOrDefault(c => c.CourseId.value == courseId);
            if (course == null)
                return NotFound();
            Availability.ValidateCourseBasedOnTimeslots(course);
            return Ok("Course is available");
        }

        [HttpPost]
        [Route("{courseId}/assign-coach")]
        public ActionResult<StatusCourse> AssignCoach(Guid courseId, [FromBody] AssignedCourseRequest dto)
        {
            var course = _myMemory.allCourses.FirstOrDefault(c => c.CourseId.value == courseId);
            if (course == null)
                return BadRequest($"Course wasn't found with {courseId}");
            Availability.CheckingCoach(course, dto.coach);
            return Ok("Coach is assigned");
        }

    }
}