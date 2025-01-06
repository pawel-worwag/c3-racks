using MediatR;
using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;

namespace Identity.Server.Application.Applications.GetDetails;

public record GetApplicationDetailsRequest : IRequest<ApplicationDto>
{
    public required string Id { get; init; }
}

internal class
    GetApplicationDetailsRequestHandler(IOpenIddictApplicationManager manager) : IRequestHandler<GetApplicationDetailsRequest, ApplicationDto>
{
    public async Task<ApplicationDto> Handle(GetApplicationDetailsRequest request, CancellationToken cancellationToken)
    {
        var app = (OpenIddictEntityFrameworkCoreApplication<string, OpenIddictEntityFrameworkCoreAuthorization, OpenIddictEntityFrameworkCoreToken>
            ?) await manager.FindByIdAsync(request.Id, cancellationToken);
        if(app is null) {throw new KeyNotFoundException("Application not found");}
        return new ApplicationDto()
        {
            Id = request.Id,
            //Properties = app.Properties,
            //Permissions = app.Permissions,
            //Requirements = app.Requirements,
            ApplicationType = app.ApplicationType,
            ClientType = app.ClientType,
            ConsentType = app.ConsentType,
            DisplayName = app.DisplayName,
            //RedirectUris = app.RedirectUris,
            //PostLogoutRedirectUris = app.PostLogoutRedirectUris,
            ClientId = app.ClientId,
            //Settings = app.Settings
        };
    }
}