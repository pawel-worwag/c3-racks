using System.Security.Claims;

namespace Identity.Server.Application.Applications.GetDetails;

public record ApplicationDto
{
    public string? Id { get; init; }
    public string? ApplicationType { get; init; }
    //ICollection<TAuthorization> Authorizations
    public string? ClientId { get; init; }
    public string? ClientType { get; init; }
    public string? ConsentType { get; init; }
    public string? DisplayName { get; init; }
    public List<ClaimDto> Permissions { get; init; } = new List<ClaimDto>();
    public List<string> PostLogoutRedirectUris { get; init; }
    public string? Properties { get; init; }
    public List<string> RedirectUris { get; init; }
    public List<ClaimDto> Requirements { get; init; } = new List<ClaimDto>();
    public string? Settings { get; set; }
}