using System.Net.Http.Json;

namespace HorsesForCourses.Blazor.Services;

public sealed class CoachFromDatabase //deze klasse stuurt http verzoeken naar backend api
{
    private readonly HttpClient _http;

    public CoachFromDatabase(HttpClient http) => _http = http;

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

    // public async Task DeleteCoach(Guid id)
    // {
    //     var response = await _http.DeleteAsync($"coaches/{id}");
    //     response.EnsureSuccessStatusCode();
    // }
}
