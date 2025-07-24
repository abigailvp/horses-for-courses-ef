using Microsoft.AspNetCore.Mvc;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.WebApi.Repo;
using HorsesForCourses.Services;

namespace CoursesController
{
    [ApiController]
    [Route("/[controller]")]
    public class CoursesController : ControllerBase
    {
        [HttpPost] // met naam en periode
        public ActionResult<string> CreateEmptyCourse([FromBody] CourseDTO dto)
        {
            var course = CourseMapper.CreateEmptyCourse(dto);
            if (course == null)
                return BadRequest("Course can't be added");
            AllData.allCourses.Add(course);
            return Ok($"Course {course.NameCourse} has been added");
        }

        [HttpPost]
        [Route("{courseId}/competences")]
        public ActionResult<string> AddCompetences(Guid courseId, [FromBody] CompetentCourseDTO dto)
        {
            var course = AllData.allCourses.FirstOrDefault(c => c.CourseId.value == courseId);
            if (course == null)
                return BadRequest();
            return Ok(course.AddCompetenceList(dto.ListOfCourseCompetences));
        }

        [HttpPost]
        [Route("{courseId}/timeslots")]
        public ActionResult<string> AddTimeslots(Guid courseId, [FromBody] ScheduledCourseDTO dto)
        {
            var course = AllData.allCourses.FirstOrDefault(c => c.CourseId.value == courseId);
            if (course == null)
                return BadRequest();
            return Ok(course.AddTimeSlotList(dto.CourseTimeslots));
        }

        [HttpPost]
        [Route("{courseId}/confirm")]
        public ActionResult<StatusCourse> ConfirmCourse(Guid courseId, [FromBody] CourseDTO dto)
        {
            var course = AllData.allCourses.FirstOrDefault(c => c.CourseId.value == courseId);
            if (course == null)
                return BadRequest();
            var result = Availability.ValidateCourseBasedOnTimeslots(course);
            if (result == StatusCourse.WaitingForMatchingTimeslots)
                return Ok(result);
            return BadRequest(result);
        }


        [HttpPost]
        [Route("{courseId}/assign-coach")]
        public ActionResult<StatusCourse> AssignCoach(Guid courseId, [FromBody] AssignedCourseDTO dto)
        {
            var course = AllData.allCourses.FirstOrDefault(c => c.CourseId.value == courseId);
            if (course == null)
                return BadRequest();
            var result = Availability.CheckingCoach(course, dto.coach);
            if (result == StatusCourse.Assigned)
                return Ok(result);
            return BadRequest(result);
        }

    }
}