using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkBasics.Options;

public sealed record DbConfigurationOptions
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public string? User { get; set; }

    [Required]
    public string? Password { get; set; }
}