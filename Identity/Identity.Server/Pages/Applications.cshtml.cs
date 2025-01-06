using Identity.Server.Models.Applications;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenIddict.Abstractions;

namespace Identity.Server.Pages;

public class Applications(IServiceProvider serviceProvider) : PageModel
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    public List<ApplicationVM> applications = new();

    public async void OnGet()
    {
        await LoadData();
    }

    public async Task LoadData()
    {
        using var scope = _serviceProvider.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
        applications.Clear();
        await foreach (var application in manager.ListAsync())
        {
            var id = await manager.GetIdAsync(application);
            var clientid = await manager.GetClientIdAsync(application);
            var type = await manager.GetApplicationTypeAsync(application);
            var permissions = (await manager.GetPermissionsAsync(application)).ToString();
            
            var app = new ApplicationVM()
            {
                Id = id,
                ClientId = clientid,
                ApplicationType = type
            };
            applications.Add(app);
        }
    }
}