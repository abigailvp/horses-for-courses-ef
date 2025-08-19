using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Paging;
using HorsesForCourses.WebApi;
using Microsoft.EntityFrameworkCore;
using Polly.Registry;
using System.Linq;

namespace HorsesForCourses.Repo;

public interface ICoursesRepo
{
    Task AddCourse(Course course);
    void RemoveCourse(Course course);
    Task<Course> GetCourseById(int id);
    Task<Course> GetSpecificCourseById(int id);
    Task<List<Course>> ListCourses();


    IQueryable<Course> OrderCoursesQuery();
    Task<PagedResult<Course>> GetCoursePages(int pageNumber, int amountOfCourses);


}

public class CoursesRepo : ICoursesRepo
{
    private readonly AppDbContext _context;
    private readonly ResiliencePipelineProvider<string> _pipeline;

    public CoursesRepo(AppDbContext context, ResiliencePipelineProvider<string> pipe)
    {
        _context = context;
        _pipeline = pipe;
    }


    public async Task AddCourse(Course course)
    => await _context.Courses.AddAsync(course);


    public IQueryable<Course> OrderCoursesQuery()
    {
        var queryablecourses = _context.Courses
                .Where(c => c.NameCourse != null)
                .OrderBy(c => c.CourseId);
        return queryablecourses;
    }

    public async Task<PagedResult<Course>> GetCoursePages(int pageNumber, int amountOfCourses)
    {
        var request = new PageRequest(pageNumber, amountOfCourses);
        var query = OrderCoursesQuery();
        return await PagingExecution.ToPagedResultAsync<Course>(query, request);
    }

    public async Task<Course> GetCourseById(int id)
    => await _context.Courses.FindAsync(id);


    public async Task<Course> GetSpecificCourseById(int id)
    {
        return await _context.Courses.Include(c => c.ListOfCourseSkills)
            .Include(c => c.CourseTimeslots)
            .Include(c => c.CoachForCourse)
            .FirstOrDefaultAsync(c => c.CourseId == id); ;

    }

    public async Task<List<Course>> ListCourses()
    {
        var pipeline = _pipeline.GetPipeline("db-pipeline");
        return await pipeline.ExecuteAsync(async token =>
        {
            return await _context.Courses.ToListAsync();
        });

    }

    public void RemoveCourse(Course course) => _context.Courses.Remove(course);


}