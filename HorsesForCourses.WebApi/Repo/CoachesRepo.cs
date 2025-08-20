using System.Threading.Tasks;
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

    IQueryable<CoachResponse> OrderAndProjectCoaches(int page, int size);
    Task<PagedResult<CoachResponse>> GetCoachPages(int numberOfPage, int amountOfCoaches);

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

    public record CoachResponse(int id, string name);
    public IQueryable<CoachResponse> OrderAndProjectCoaches(int page, int size)
    {
        var queryablecoaches = _context.Coaches
                .Where(p => p.NameCoach != null)
                .OrderBy(p => p.CoachId)
                .Select(a => new CoachResponse(
                        a.CoachId,
                        a.NameCoach
                ));

        return queryablecoaches;
    }

    public async Task<PagedResult<CoachResponse>> GetCoachPages(int numberOfPage, int amountOfCoaches)
    {
        var request = new PageRequest(numberOfPage, amountOfCoaches);
        var query = OrderAndProjectCoaches(numberOfPage, amountOfCoaches);
        return await PagingExecution.ToPagedResultAsync<CoachResponse>(query, request);
    }


    public async Task<Coach> GetCoachById(int id)
    {
        return await _context.Coaches.FindAsync(id);
        //of FirstOrDefaultAsync(c => c.CoachId == id)       
    }

    public record SpecificCoach(int id, string name, string email, int numberOfAssignedCourses);
    public IQueryable<SpecificCoach> GetCoachReponseById(int id)//nog niet geïmplementeerde projectie
    {
        return _context.Coaches
                            .AsNoTracking()
                            .Where(c => c.CoachId == id)
                            .OrderBy(p => p.CoachId)
                            .Select(a => new SpecificCoach(
                                    a.CoachId,
                                    a.NameCoach,
                                    a.Email,
                                    a.ListOfCoursesAssignedTo.Count()
                            ));

    }


    public record DetailedCoach(int Id, string Name, string Email, IReadOnlyList<Skill> listOfSkills, IReadOnlyList<AssignedCourse> listOfCourses);
    public record AssignedCourse(int Id, string Name);
    public async Task<DetailedCoach?> GetSpecificCoachById(int id) //nog aanpassen
    {
        return await _context.Coaches
            .AsNoTracking()
            .Where(c => c.CoachId == id)
            .OrderBy(c => c.CoachId)
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