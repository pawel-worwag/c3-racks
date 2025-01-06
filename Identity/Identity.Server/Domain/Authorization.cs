using OpenIddict.EntityFrameworkCore.Models;

namespace Identity.Server.Domain;

public class Authorization : OpenIddictEntityFrameworkCoreAuthorization<Guid, Domain.Application, Domain.Token>
{
    
}