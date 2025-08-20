using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace HorsesForCourses.Blazor.Services;

public sealed class Coach
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

}