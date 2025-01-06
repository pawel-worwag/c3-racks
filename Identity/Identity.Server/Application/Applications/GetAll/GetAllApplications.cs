using MediatR;
using OpenIddict.Abstractions;

namespace Identity.Server.Application.Applications.GetAll;

public record GetAllApplicationsRequest : IRequest<List<ApplicationDto>>
{
    
}

internal class GetAllApplicationsRequestHandler(IOpenIddictApplicationManager manager)
    : IRequestHandler<GetAllApplicationsRequest, List<ApplicationDto>>
{
    
    public async Task<List<ApplicationDto>> Handle(GetAllApplicationsRequest request, CancellationToken cancellationToken)
    {
        var apps = new List<ApplicationDto>();
        await foreach (var application in manager.ListAsync())
        {
            var id = await manager.GetIdAsync(application);
            var clientId = await manager.GetClientIdAsync(application);
            var applicationType = await manager.GetApplicationTypeAsync(application);
            var displayName = await manager.GetDisplayNameAsync(application);
            apps.Add(new ApplicationDto()
            {
                Id = id,
                ClientId = clientId,
                ApplicationType = applicationType,
                DisplayName = displayName
            });
        }
        return apps;
    }
}