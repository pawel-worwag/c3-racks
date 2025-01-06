using System.Reflection;
using Identity.Server.Application.Applications.GetAll;
using Identity.Server.Application.Applications.GetDetails;
using Identity.Server.Domain;
using Identity.Server.Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quartz;
using static OpenIddict.Abstractions.OpenIddictConstants;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddPageRoute(
        "/", "Home");
});
builder.Services.AddOpenApi();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
Console.WriteLine($"SC: Filename={Path.Combine(Path.GetTempPath(), "openiddict-balosar-server.sqlite3")}");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite($"Filename={Path.Combine(Path.GetTempPath(), "openiddict-balosar-server.sqlite3")}");
    options.UseOpenIddict();
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();
builder.Services.AddQuartz(options =>
{
    options.UseSimpleTypeLoader();
    options.UseInMemoryStore();
});
builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>();
        options.UseQuartz();
    })
    .AddClient(options =>
            {
                options.AllowAuthorizationCodeFlow();
                options.AddDevelopmentEncryptionCertificate()
                       .AddDevelopmentSigningCertificate();
                options.UseAspNetCore()
                       .EnableStatusCodePagesIntegration()
                       .EnableRedirectionEndpointPassthrough();
                options.UseSystemNetHttp()
                       .SetProductInformation(Assembly.GetExecutingAssembly());
                options.UseWebProviders()
                       .AddGitHub(options =>
                       {
                           options.SetClientId("c4ade52327b01ddacff3")
                                  .SetClientSecret("da6bed851b75e317bf6b2cb67013679d9467c122")
                                  .SetRedirectUri("callback/login/github");
                       });
            })
    .AddServer(options =>
    {
        options.SetAuthorizationEndpointUris("connect/authorize")
            .SetEndSessionEndpointUris("connect/logout")
            .SetTokenEndpointUris("connect/token")
            .SetUserInfoEndpointUris("connect/userinfo");
        options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles);
        options.AllowAuthorizationCodeFlow()
            .AllowRefreshTokenFlow();
        options.AddDevelopmentEncryptionCertificate()
            .AddDevelopmentSigningCertificate();
        options.UseAspNetCore()
            .EnableAuthorizationEndpointPassthrough()
            .EnableEndSessionEndpointPassthrough()
            .EnableStatusCodePagesIntegration()
            .EnableTokenEndpointPassthrough();
    })
    .AddValidation(options =>
    {
        options.UseLocalServer();
        options.UseAspNetCore();
    });


var app = builder.Build();

app.UseRouting();
app.MapStaticAssets();
app.UseAuthentication();
app.UseAuthorization();

app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "v1");
});

app.MapRazorPages();
app.MapGet("/api/identity/applications", async (IMediator m) => await m.Send(new GetAllApplicationsRequest()));
app.MapGet("/api/identity/applications/{id}", async (IMediator m, String id) => await m.Send(new GetApplicationDetailsRequest(){Id = id}));
app.Run();