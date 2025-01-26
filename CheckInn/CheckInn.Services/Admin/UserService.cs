using System.Linq;
using CheckInn.DataAccess.Data;
using CheckInn.Models;
using CheckInn.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CheckInn.Services.Admin;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _db;

    public UserService(ApplicationDbContext db)
    {
        _db = db;
    }

    [Authorize(Roles = "Admin")]
    public List<UserProjection> GetAllUsers()
    {
        return _db.Users.Select(user => new UserProjection
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Reservations = user.Reservations,
            EmailConfirmed = user.EmailConfirmed,
            Email = user.Email,
            roles = _db.UserRoles
                .Where(ur => ur.UserId == user.Id)
                .Join(_db.Roles,
                    ur => ur.RoleId,
                    role => role.Id,
                    (ur, role) => role)
                .ToList()
        }).ToList();
    }

    public List<IdentityRole> GetPossibleRoles()
    {
        return _db.Roles.ToList();
    }

    public bool AddRoleToUser(string requestEmail, string requestRoleName)
    {
        var user = _db.Users.FirstOrDefault(u => u.Email == requestEmail);
        if (user == null)
        {
            return false; 
        }

        var role = _db.Roles.FirstOrDefault(r => r.Name == requestRoleName);
        if (role == null)
        {
            return false; 
        }

        var userRole = _db.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id && ur.RoleId == role.Id);
        if (userRole != null)
        {
            return false;
        }

        _db.UserRoles.Add(new IdentityUserRole<string>
        {
            UserId = user.Id,
            RoleId = role.Id
        });

        _db.SaveChanges();
        return true;
    }

    public bool RemoveRoleFromUser(string requestEmail, string requestRoleName)
    {
        var user = _db.Users.FirstOrDefault(u => u.Email == requestEmail);
        if (user == null)
        {
            return false;
        }

        var role = _db.Roles.FirstOrDefault(r => r.Name == requestRoleName);
        if (role == null)
        {
            return false;
        }

        var userRole = _db.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id && ur.RoleId == role.Id);
        if (userRole == null)
        {
            return false;
        }

        _db.UserRoles.Remove(userRole);
        _db.SaveChanges();
        return true;
    }

    public bool RemoveReservation(int requestReservationId)
    {
        var reservation = _db.Reservations.FirstOrDefault(r => r.Id == requestReservationId);
        if (reservation == null)
        {
            return false;
        }

        _db.Reservations.Remove(reservation);
        _db.SaveChanges();
        return true;
    }
}
