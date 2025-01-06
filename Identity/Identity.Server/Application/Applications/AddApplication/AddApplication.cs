using MediatR;
using OpenIddict.Abstractions;

namespace Identity.Server.Application.Applications.AddApplication;

public record AddApplicationRequest : IRequest<Guid>
{
    public required string DisplayName { get; init; }
}

internal class AddApplicationRequestHandler(IOpenIddictApplicationManager manager) : IRequestHandler<AddApplicationRequest, Guid>
{
    public async Task<Guid> Handle(AddApplicationRequest request, CancellationToken cancellationToken)
    {
        var app = new OpenIddictApplicationDescriptor()
        {
            ApplicationType = OpenIddictConstants.ApplicationTypes.Web,
            ClientId = Guid.NewGuid().ToString(),
            ConsentType = OpenIddictConstants.ConsentTypes.Explicit,
            DisplayName = request.DisplayName,
            ClientType = OpenIddictConstants.ClientTypes.Public,
            PostLogoutRedirectUris =
            {
                new Uri("https://localhost:44310/authentication/logout-callback")
            },
            RedirectUris =
            {
                new Uri("https://localhost:44310/authentication/login-callback")
            },
            Permissions =
            {
                OpenIddictConstants.Permissions.Endpoints.Authorization,
                OpenIddictConstants.Permissions.Endpoints.Token,
                OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                OpenIddictConstants.Permissions.ResponseTypes.Code,
                OpenIddictConstants.Permissions.Scopes.Email,
                OpenIddictConstants.Permissions.Scopes.Profile,
                OpenIddictConstants.Permissions.Scopes.Roles
            },
            Requirements =
            {
                OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
            }
        };
        var result = (Domain.Application)await manager.CreateAsync(app, cancellationToken);
        return result.Id;
    }
}