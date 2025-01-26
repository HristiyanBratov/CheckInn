using System.Security.Claims;
using CheckInn.Models;
using CheckInn.Models.ViewModels;
using CheckInn.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CheckInn.Controllers;

public class AdminController : Controller
{
    private readonly IUserService _userService;

    public AdminController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = "Admin")]
    public IActionResult AdminPanel()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (User.IsInRole("Admin"))
        {
            AdminViewModel adminViewModel = new AdminViewModel()
            {
                users = _userService.GetAllUsers() ,
                roles = _userService.GetPossibleRoles()
            };
            return View(adminViewModel);
        }
        else
        {
            //Return to home page
            return RedirectToAction("Index");
        }
    }
    // Add Role Endpoint
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddRole([FromBody] AddRoleRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.RoleName))
            {
                return BadRequest("Invalid email or role name.");
            }

            var result = _userService.AddRoleToUser(request.Email, request.RoleName);

            if (result)
            {
                return Ok();
            }

            return StatusCode(500, "Failed to add role.");
        }

        // Remove Role Endpoint
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveRole([FromBody] RemoveRoleRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.RoleName))
            {
                return BadRequest("Invalid email or role name.");
            }

            var result = _userService.RemoveRoleFromUser(request.Email, request.RoleName);

            if (result)
            {
                return Ok();
            }

            return StatusCode(500, "Failed to remove role.");
        }

        // Remove Reservation Endpoint
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveReservation([FromBody] RemoveReservationRequest request)
        {
            if (request.ReservationId <= 0)
            {
                return BadRequest("Invalid reservation ID.");
            }

            var result = _userService.RemoveReservation(request.ReservationId);

            if (result)
            {
                return Ok();
            }

            return StatusCode(500, "Failed to remove reservation.");
        }
    }

    // Request Models
    public class AddRoleRequest
    {
        public string Email { get; set; }
        public string RoleName { get; set; }
    }

    public class RemoveRoleRequest
    {
        public string Email { get; set; }
        public string RoleName { get; set; }
    }

    public class RemoveReservationRequest
    {
        public int ReservationId { get; set; }
    }
