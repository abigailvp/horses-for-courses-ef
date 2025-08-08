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
    Task<int> CompleteAsync();

}

public class Repo : IRepo
{
    private readonly AppDbContext _context;
    public Repo(AppDbContext context) => _context = context;

    public async Task<bool> StartAsync()
    => await _context.Database.EnsureCreatedAsync();


    public async Task AddCoach(Coach coach)
    => await _context.Coaches.AddAsync(coach);


    public async Task<Coach> GetCoachById(int id)
    => await _context.Coaches.FindAsync(id);
    //of FirstOrDefaultAsync(c => c.CoachId == id)


    public async Task<Coach> GetSpecificCoachById(int id)
    {
        return await _context.Coaches.Include(c => c.ListOfCompetences)
            .Include(c => c.ListOfCoursesAssignedTo)
            .FirstOrDefaultAsync(c => c.CoachId == id);

    }

    public async Task<List<Coach>> ListCoaches()
    {
        return await _context.Coaches.ToListAsync();
    }

    public void RemoveCoach(Coach coach)
    {
        _context.Coaches.Remove(coach);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}