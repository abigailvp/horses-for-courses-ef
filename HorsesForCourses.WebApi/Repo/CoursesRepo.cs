using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.Paging;
using HorsesForCourses.WebApi;
using Microsoft.EntityFrameworkCore;
using Polly.Registry;
using static HorsesForCourses.Repo.CoursesRepo;

namespace HorsesForCourses.Repo;

public interface ICoursesRepo
{
    Task AddCourse(Course course);
    Task<int> RemoveCourse(int id);
    Task<Course?> GetCourseById(int id);
    Task<DetailedCourse?> GetSpecificCourseById(int id);
    Task<IReadOnlyList<CourseResponse>> ListCompactCourses();


    IQueryable<CourseResponse> OrderCoursesQuery();
    Task<PagedResult<CourseResponse>> GetCoursePages(int pageNumber, int amountOfCourses);

    Task DeleteCourseWithoutDates(int id);


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


    public IQueryable<CourseResponse> OrderCoursesQuery()
    {
        var queryablecourses = _context.Courses
                .Where(c => c.NameCourse != null)
                .OrderBy(c => c.CourseId)
                .Select(c => new CourseResponse(c.CourseId, c.NameCourse, c.StartDateCourse, c.EndDateCourse));
        return queryablecourses;
    }

    public async Task<PagedResult<CourseResponse>> GetCoursePages(int pageNumber, int amountOfCourses)
    {
        var request = new PageRequest(pageNumber, amountOfCourses);
        var query = OrderCoursesQuery();
        return await PagingExecution.ToPagedResultAsync<CourseResponse>(query, request);
    }

    public async Task<Course?> GetCourseById(int id)
    => await _context.Courses.FindAsync(id);


    public record DetailedCourse(int Id, string Name, DateOnly startDate, DateOnly endDate, IReadOnlyList<Skill> skills,
    IReadOnlyList<shortTimeslot> ListOfTimeslots, CoachForCourseResponse? assignedCoach);
    public record shortTimeslot(string Day, int beginhour, int endhour);
    public record CoachForCourseResponse(int? id, string? name);

    public async Task<DetailedCourse?> GetSpecificCourseById(int id)
    {
        return await _context.Courses.AsNoTracking()
                                    .Where(d => d.CourseId != 0)
                                    .OrderBy(d => d.CourseId).ThenBy(d => d.NameCourse)
                                    .Select(d => new DetailedCourse(
                                        d.CourseId,
                                        d.NameCourse,
                                        d.StartDateCourse,
                                        d.EndDateCourse,
                                        d.ListOfCourseSkills,
                                        d.CourseTimeslots
                                            .OrderBy(t => t.BeginTimeslot)
                                            .Select(t => new shortTimeslot(
                                                t.Day,
                                                t.BeginTimeslot,
                                                t.EndTimeslot))
                                            .ToList(),
                                        d.CoachForCourse == null ? null : new CoachForCourseResponse(
                                            d.CoachForCourse.CoachId,
                                            d.CoachForCourse.NameCoach)))
                                    .FirstOrDefaultAsync();
    }

    public record CourseResponse(int CourseId, string NameCourse, DateOnly StartDateCourse, DateOnly EndDateCourse);

    public async Task<IReadOnlyList<CourseResponse>> ListCompactCourses()
    {
        var pipeline = _pipeline.GetPipeline("db-pipeline");
        return await pipeline.ExecuteAsync(async token =>
        {
            return await _context.Courses
        .AsNoTracking()
        .Where(p => !string.IsNullOrEmpty(p.NameCourse))
        .OrderBy(p => p.NameCourse).ThenBy(p => p.CourseId)
        .Select(p => new CourseResponse(p.CourseId, p.NameCourse, p.StartDateCourse, p.EndDateCourse))
        .ToListAsync();

        });
    }


    public async Task<int> RemoveCourse(int id) => await _context.Courses.Where(c => c.CourseId == id).ExecuteDeleteAsync();

    public async Task DeleteCourseWithoutDates(int id)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM Courses WHERE CourseId = {id}");

    }


}