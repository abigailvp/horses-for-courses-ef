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
        => Ok(AllData.CreateEmptyCourse(dto));

        [HttpPost]
        [Route("{courseId}/competences")]
        public ActionResult<string> AddCompetences(Guid courseId, [FromBody] CompetentCourseDTO dto)
        {
            var course = AllData.allCourses.FirstOrDefault(c => c.CourseId.value == courseId);
            return Ok(course.AddCompetenceList(dto.ListOfCourseCompetences));
        }

        [HttpPost]
        [Route("{courseId}/timeslots")]
        public ActionResult<string> AddTimeslots(Guid courseId, [FromBody] ScheduledCourseDTO dto)
        {
            var course = AllData.allCourses.FirstOrDefault(c => c.CourseId.value == courseId);
            return Ok(course.AddTimeSlotList(dto.CourseTimeslots));
        }

        [HttpPost]
        [Route("{courseId}/confirm")]
        public ActionResult<StatusCourse> ConfirmCourse(Guid courseId, [FromBody] CourseDTO dto)
        {
            var course = AllData.allCourses.FirstOrDefault(c => c.CourseId.value == courseId);
            return Ok(Availability.ValidateCourseBasedOnTimeslots(course));
        }


        // [HttpPost]
        // [Route("{courseId}/assign-coach")]
        // public ActionResult<string> AssignCoach(Guid courseId, [FromBody] CourseDTO dto)
        // {
        //     var course = AllData.allCourses.FirstOrDefault(c => c.CourseId.value == courseId);

        // }

    }
}