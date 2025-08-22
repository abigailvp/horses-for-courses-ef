using System.Net.Http.Json;

namespace HorsesForCourses.Blazor.Services;

public sealed class FromDatabase //deze klasse stuurt http verzoeken naar backend api
{
    private readonly HttpClient _http;

    public FromDatabase(HttpClient http) => _http = http;

    public async Task<IReadOnlyList<Coach>> GetCoaches()
    {
        var list = await _http.GetFromJsonAsync<IReadOnlyList<Coach>>("Coaches")!;
        if (list == null)
            return [];
        return list;
    }
    public async Task AddCoach(CreateCoachRequest req)
    {
        var response = await _http.PostAsJsonAsync("Coaches", req);
        response.EnsureSuccessStatusCode();
    }

    public async Task<IReadOnlyList<AssignedCoach>> GetAssignedCoaches()
    {
        var list = await _http.GetFromJsonAsync<IReadOnlyList<AssignedCoach>>("Coaches/assigned")!;
        if (list == null)
            return [];
        return list;
    }

    public async Task<IReadOnlyList<AssignedCourse>> GetAssignedCourses()
    {
        var list = await _http.GetFromJsonAsync<IReadOnlyList<AssignedCourse>>("Courses/Assigned")!;
        if (list == null)
            return [];
        return list;
    }


}
