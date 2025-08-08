using HorsesForCourses.WebApi;

public interface IUnitOfWork : IDisposable
{
    IRepo Objects { get; } //wordt ingevuld in uow
    Task CompleteAsync();
}


public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IRepo Objects { get; }


    public UnitOfWork(AppDbContext context, IRepo coachesOrCourses)
    {
        _context = context;
        Objects = coachesOrCourses;
    }


    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose() => _context.Dispose(); //gc ruimt lege objecten al op

}