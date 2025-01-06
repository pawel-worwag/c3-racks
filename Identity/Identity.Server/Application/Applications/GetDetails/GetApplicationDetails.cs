using System.Text.Json;
using System.Text.Json.Serialization;
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
        var app = (Domain.Application?) await manager.FindByIdAsync(request.Id, cancellationToken);
        if(app is null) {throw new KeyNotFoundException("Application not found");}

        var permissions = JsonSerializer.Deserialize<List<string>>(app.Permissions).Select(p=>new ClaimDto(){Type = p.Split(":")[0], Value = p.Split(":")[1]}).ToList();
        var requirements = JsonSerializer.Deserialize<List<string>>(app.Requirements).Select(p=>new ClaimDto(){Type = p.Split(":")[0], Value = p.Split(":")[1]}).ToList();
        var redirectUris = JsonSerializer.Deserialize<List<string>>(app.RedirectUris);
        var postLogoutRedirectUris = JsonSerializer.Deserialize<List<string>>(app.PostLogoutRedirectUris);
        return new ApplicationDto()
        {
            Id = request.Id,
            Properties = app.Properties,
            Permissions = permissions,
            Requirements = requirements,
            ApplicationType = app.ApplicationType,
            ClientType = app.ClientType,
            ConsentType = app.ConsentType,
            DisplayName = app.DisplayName,
            RedirectUris = redirectUris,
            PostLogoutRedirectUris = postLogoutRedirectUris,
            ClientId = app.ClientId,
            Settings = app.Settings
        };
    }
}