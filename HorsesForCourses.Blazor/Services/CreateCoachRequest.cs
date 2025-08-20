using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace HorsesForCourses.Blazor.Services;

public sealed class CreateCoachRequest
{
    [MaxLength(100)]
    [Required]
    public string Name { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; }
}