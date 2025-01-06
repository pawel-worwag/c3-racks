namespace Identity.Server.Application.Applications.GetAll;

public record ApplicationDto
{
    public string? Id { get; init; }
    public string? ClientId { get; init; }
    public string? ApplicationType { get; init; }
    public string? DisplayName { get; init; }
}