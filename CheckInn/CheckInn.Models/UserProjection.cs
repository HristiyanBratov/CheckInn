using Microsoft.AspNetCore.Identity;

namespace CheckInn.Models;

public class UserProjection
{
    public string FirstName { get; set; } = string.Empty;

    
    public string LastName { get; set; } = string.Empty;

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    
    public bool EmailConfirmed { get; set; }
    
    public string Email { get; set; } = string.Empty;
    
    public List<IdentityRole> roles { get; set; } = new List<IdentityRole>();
}