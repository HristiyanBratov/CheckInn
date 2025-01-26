using Microsoft.AspNetCore.Identity;

namespace CheckInn.Models.ViewModels;

public class AdminViewModel
{
    public List<UserProjection> users { get; set; }
    public List<IdentityRole> roles { get; set; }
    
}