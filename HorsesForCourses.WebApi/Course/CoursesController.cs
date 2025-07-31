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
        public ActionResult<int> CreateEmptyCourse([FromBody] CourseRequest dto)
        {
            var course = new Course(dto.NameCourse, DateOnly.Parse(dto.StartDateCourse), DateOnly.Parse(dto.EndDateCourse));
            //omzetten naar DateOnly

            _myMemory.allCourses.Add(course);
            return Ok(course.CourseId);
        }

        [HttpPost]
        [Route("{Id}/skills")] //de id gaat van in de url naar de methode
        public IActionResult AddCompetences(int Id, [FromBody] CompetentCourseRequest dto)
        {
            var course = _myMemory.allCourses.FirstOrDefault(c => c.CourseId == Id);
            if (course == null)
                return NotFound();
            course.AddCompetenceList(dto.ListOfCourseCompetences);
            return Ok();
        }

        [HttpPost]
        [Route("{Id}/timeslots")]
        public IActionResult AddTimeslots(int Id, [FromBody] ScheduledCourseRequest dto) //invoer van lijstdag moet format "YYYY/MM/DD" hebben
        {
            var course = _myMemory.allCourses.FirstOrDefault(c => c.CourseId == Id);
            if (course == null)
                return NotFound();
            course.AddTimeSlotList(CourseMapper.ConvertToDomainList(dto.CourseTimeslots));
            return Ok();
        }

        [HttpPost]
        [Route("{Id}/confirm")]
        public IActionResult ConfirmCourse(int Id)
        {
            var course = _myMemory.allCourses.FirstOrDefault(c => c.CourseId == Id);
            if (course == null)
                return NotFound();
            course.ValidateCourseBasedOnTimeslots(course);
            return Ok();
        }

        [HttpPost]
        [Route("{Id}/assign-coach")]
        public IActionResult AssignCoach(int Id, [FromBody] AssignedCourseRequest dto)
        {
            var course = _myMemory.allCourses.FirstOrDefault(c => c.CourseId == Id);
            if (course == null)
                return NotFound();
            var coach = _myMemory.allCoaches.FirstOrDefault(c => c.CoachId == dto.coachId);
            if (coach == null)
                return NotFound();
            course.AddingCoach(course, coach);
            return Ok();
        }

        [HttpGet]
        public ActionResult<AllCoursesResponse> GetCourses()
        {
            var allCourses = _myMemory.allCourses;
            return CourseMapper.ConvertToListCourses(allCourses);
        }



        [HttpGet]
        [Route("{Id}")]
        public ActionResult<DetailedCourseResponse> GetCourseById(int Id)
        {
            var course = _myMemory.allCourses.Where(c => c.CourseId == Id).FirstOrDefault();
            if (course == null)
                return NotFound();
            return Ok(CourseMapper.ConvertToDetailedCourse(course));
        }




    }
}