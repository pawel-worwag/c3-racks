using OpenIddict.EntityFrameworkCore.Models;

namespace Identity.Server.Domain;

public class Application : OpenIddictEntityFrameworkCoreApplication<Guid, Domain.Authorization, Domain.Token>
{
    
}