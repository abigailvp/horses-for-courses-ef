using System.Threading.Tasks;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.WebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

public class UnitOfWork : IDisposable, IRepo
{
    private readonly AppDbContext _context;
    private bool _disposed = false;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }


    public async Task<bool> StartAsync()
    {
        return await _context.Database.EnsureCreatedAsync();
    }

    public void AddCoach(Coach coach)
    {
        _context.Coaches.Add(coach);
    }

    public async Task<Coach> GetCoachById(int id)
    {
        return await _context.Coaches.FirstOrDefaultAsync(c => c.CoachId == id);
        //of findasync met id
    }

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

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose() //van IDisposable
    {
        Dispose(true);
        GC.SuppressFinalize(this); //garbagecollector
    }



}