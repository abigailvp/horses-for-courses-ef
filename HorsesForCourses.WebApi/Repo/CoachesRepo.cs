using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.Paging;
using HorsesForCourses.WebApi;
using Microsoft.EntityFrameworkCore;
using Polly.Registry;
using static HorsesForCourses.Repo.CoachesRepo;

namespace HorsesForCourses.Repo;

public interface ICoachesRepo
{
    Task AddCoach(Coach coach);
    void RemoveCoach(Coach coach);
    Task<Coach> GetCoachById(int id);
    Task<DetailedCoach?> GetSpecificCoachById(int id);
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


    public record DetailedCoach(int Id, string Name, string Email, IReadOnlyList<Skill> listOfSkills, IReadOnlyList<AssignedCourse> listOfCourses);
    public record AssignedCourse(int Id, string Name);
    public async Task<DetailedCoach?> GetSpecificCoachById(int id)
    {
        return await _context.Coaches
            .AsNoTracking()
            .Where(c => c.CoachId == id)
            .Select(c => new DetailedCoach(
                c.CoachId,
                c.NameCoach,
                c.Email,
                c.ListOfCompetences,
                c.ListOfCoursesAssignedTo
                    .Select(a => new AssignedCourse(
                        a.CourseId,
                        a.NameCourse))
                    .ToList()))
            .SingleOrDefaultAsync();
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