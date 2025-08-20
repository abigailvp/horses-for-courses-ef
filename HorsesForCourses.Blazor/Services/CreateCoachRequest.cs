using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace HorsesForCourses.Blazor.Services;

public sealed class CreateCoachRequest
{
    [Required]
    [MaxLength(100)]
    public string NameCoach { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; }
}