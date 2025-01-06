using OpenIddict.EntityFrameworkCore.Models;

namespace Identity.Server.Domain;

public class Token : OpenIddictEntityFrameworkCoreToken<Guid, Domain.Application, Domain.Authorization>
{
    
}