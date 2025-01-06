namespace Identity.Server.Application.Applications.GetDetails;

public record ClaimDto
{
    public required string Type { get; init; }
    public required string Value { get; init; }
}