namespace Identity.Server.Application.Applications.AddApplication;

public record ApplicationDto
{
    public required string DisplayName { get; init; }
}