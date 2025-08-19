using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Paging;
using HorsesForCourses.WebApi;
using Microsoft.EntityFrameworkCore;
using Polly.Registry;

namespace HorsesForCourses.Repo;

public interface ICoachesRepo
{
    Task AddCoach(Coach coach);
    void RemoveCoach(Coach coach);
    Task<Coach> GetCoachById(int id);
    Task<Coach> GetSpecificCoachById(int id);
    Task<List<Coach>> ListCoaches();

    IQueryable<Coach> OrderCoachesQuery();
    Task<PagedResult<Coach>> GetCoachPages(int numberOfPage, int amountOfCoaches);

}

public class CoachesRepo : ICoachesRepo
{
    private readonly AppDbContext _context;
    private readonly ResiliencePipelineProvider<string> _pipeline;

    public CoachesRepo(AppDbContext context, ResiliencePipelineProvider<string> pipe)
    {
        _context = context;
        _pipeline = pipe;
    }



    public async Task AddCoach(Coach coach)
    => await _context.Coaches.AddAsync(coach);


    public IQueryable<Coach> OrderCoachesQuery()
    {
        var queryablecoaches = _context.Coaches
                .Where(p => p.NameCoach != null)
                .OrderBy(p => p.CoachId);
        return queryablecoaches;
    }

    public async Task<PagedResult<Coach>> GetCoachPages(int numberOfPage, int amountOfCoaches)
    {
        var request = new PageRequest(numberOfPage, amountOfCoaches);
        var query = OrderCoachesQuery();
        return await PagingExecution.ToPagedResultAsync<Coach>(query, request);
    }

    public async Task<Coach> GetCoachById(int id)
    {
        return await _context.Coaches.FindAsync(id);
        //of FirstOrDefaultAsync(c => c.CoachId == id)       
    }



    public async Task<Coach> GetSpecificCoachById(int id)
    {
        return await _context.Coaches.Include(c => c.ListOfCompetences)
            .Include(c => c.ListOfCoursesAssignedTo)
            .FirstOrDefaultAsync(c => c.CoachId == id);

    }

    public async Task<List<Coach>> ListCoaches()
    {
        var pipeline = _pipeline.GetPipeline("db-pipeline");

        return await pipeline.ExecuteAsync(async token =>
        {
            return await _context.Coaches.ToListAsync();
        });
    }

    public void RemoveCoach(Coach coach) => _context.Coaches.Remove(coach);



}