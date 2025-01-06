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
    public string? Permissions { get; init; }
    public string? PostLogoutRedirectUris { get; init; }
    public string? Properties { get; init; }
    public string? RedirectUris { get; init; }
    public string? Requirements { get; init; }
    public string? Settings { get; set; }
}