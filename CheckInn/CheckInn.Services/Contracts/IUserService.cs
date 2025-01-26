using CheckInn.Models;
using Microsoft.AspNetCore.Identity;

namespace CheckInn.Services.Contracts;

public interface IUserService
{
    public List<UserProjection> GetAllUsers();
    public List<IdentityRole> GetPossibleRoles();

    bool AddRoleToUser(string requestEmail, string requestRoleName);
    bool RemoveRoleFromUser(string requestEmail, string requestRoleName);
    bool RemoveReservation(int requestReservationId);
}