namespace Identity.Server.Models.Applications;

public class ApplicationVM
{
    public String Id { get; init; }
    public String ApplicationType { get; set; }
    public String ClientId { get; set; }
    public String DisplayName { get; set; }
    public String Permissions { get; set; }
}