using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.WebApi;
using Microsoft.EntityFrameworkCore;

public interface IRepo
{
    Task<bool> StartAsync();
    Task AddCoach(Coach coach);
    void RemoveCoach(Coach coach);
    Task<Coach> GetCoachById(int id);
    Task<Coach> GetSpecificCoachById(int id);
    Task<List<Coach>> ListCoaches();

    Task AddCourse(Course course);
    Task<Course> GetCourseById(int id);
    Task<Course> GetSpecificCourseById(int id);
    Task<List<Course>> ListCourses();
    Task<int> CompleteAsync();

}

public class Repo : IRepo
{
    private readonly AppDbContext _context;
    public Repo(AppDbContext context) => _context = context; //constructor

    public async Task<bool> StartAsync()
    => await _context.Database.EnsureCreatedAsync();


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
        return await _context.Coaches.ToListAsync();
    }

    public async Task<List<Course>> ListCourses()
    => await _context.Courses.ToListAsync();


    public void RemoveCoach(Coach coach)
    {
        _context.Coaches.Remove(coach);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}