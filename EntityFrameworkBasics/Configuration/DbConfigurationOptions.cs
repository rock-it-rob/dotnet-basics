namespace EntityFrameworkBasics.Configuration;

public sealed record DbConfigurationOptions
{
    public string? Name { get; set; }
    public string? User { get; set; }
    public string? Password { get; set; }

    public void Validate()
    {
        if (Name is null)
            throw new ArgumentNullException(nameof(Name));
        if (User is null)
            throw new ArgumentNullException(nameof(User));
        if (Password is null)
            throw new ArgumentNullException(nameof(Password));
    }
}