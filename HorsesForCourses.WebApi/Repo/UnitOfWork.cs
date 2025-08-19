using HorsesForCourses.WebApi;

namespace HorsesForCourses.Repo;

public interface IUnitOfWork : IDisposable
{
    ICoachesRepo Coaches { get; } //wordt ingevuld in uow
    ICoursesRepo Courses { get; }
    Task CompleteAsync();
}


public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public ICoachesRepo Coaches { get; }
    public ICoursesRepo Courses { get; }


    public UnitOfWork(AppDbContext context, ICoachesRepo coaches, ICoursesRepo courses)
    {
        _context = context;
        Coaches = coaches;
        Courses = courses;
    }


    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose() => _context.Dispose(); //gc ruimt lege objecten al op

}