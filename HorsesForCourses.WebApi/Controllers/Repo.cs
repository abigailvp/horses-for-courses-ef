using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.WebApi;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Registry;
using Polly.Retry;

public interface IRepo
{
    Task AddCoach(Coach coach);
    void RemoveCoach(Coach coach);
    Task<Coach> GetCoachById(int id);
    Task<Coach> GetSpecificCoachById(int id);
    Task<List<Coach>> ListCoaches();

    Task AddCourse(Course course);
    Task<Course> GetCourseById(int id);
    Task<Course> GetSpecificCourseById(int id);
    Task<List<Course>> ListCourses();


}

public class Repo : IRepo
{
    private readonly AppDbContext _context;
    private readonly ResiliencePipelineProvider<string> _pipeline;

    public Repo(AppDbContext context, ResiliencePipelineProvider<string> pipe)
    {
        _context = context;
        _pipeline = pipe;
    }



    public async Task AddCoach(Coach coach)
    => await _context.Coaches.AddAsync(coach);

    public async Task AddCourse(Course course)
    => await _context.Courses.AddAsync(course);


    public async Task<Coach> GetCoachById(int id)
    {
        return await _context.Coaches.FindAsync(id);
        //of FirstOrDefaultAsync(c => c.CoachId == id)       
    }

    public async Task<Course> GetCourseById(int id)
    => await _context.Courses.FindAsync(id);



    public async Task<Coach> GetSpecificCoachById(int id)
    {
        return await _context.Coaches.Include(c => c.ListOfCompetences)
            .Include(c => c.ListOfCoursesAssignedTo)
            .FirstOrDefaultAsync(c => c.CoachId == id);

    }

    public async Task<Course> GetSpecificCourseById(int id)
    {
        return await _context.Courses.Include(c => c.ListOfCourseSkills)
            .Include(c => c.CourseTimeslots)
            .Include(c => c.CoachForCourse)
            .FirstOrDefaultAsync(c => c.CourseId == id); ;

    }

    public async Task<List<Coach>> ListCoaches()
    {
        var pipeline = _pipeline.GetPipeline("db-pipeline");

        return await pipeline.ExecuteAsync(async token =>
        {
            return await _context.Coaches.ToListAsync();
        });
    }

    public async Task<List<Course>> ListCourses()
    {
        var pipeline = _pipeline.GetPipeline("db-pipeline");
        return await pipeline.ExecuteAsync(async token =>
        {
            return await _context.Courses.ToListAsync();
        });

    }
    public void RemoveCoach(Coach coach)
    {
        _context.Coaches.Remove(coach);
    }


}