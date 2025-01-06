using Identity.Server.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Server.Infrastructure.Database;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options);