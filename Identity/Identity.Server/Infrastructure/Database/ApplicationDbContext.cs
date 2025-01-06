using Identity.Server.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Server.Infrastructure.Database;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<Domain.User>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.UseOpenIddict<Domain.Application, Domain.Authorization, Domain.Scope, Domain.Token, Guid>();
    }
}