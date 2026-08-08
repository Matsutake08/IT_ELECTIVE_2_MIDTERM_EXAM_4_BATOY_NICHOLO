using System.Security.Claims;
using ComputerLaboratoryUsageMonitoringSystem.Models;
using ComputerLaboratoryUsageMonitoringSystem.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerLaboratoryUsageMonitoringSystem.Controllers;

public class AccountController : Controller
{
    private readonly UserRepository _userRepository;

    public AccountController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public IActionResult Register(User user)
    {
        if (_userRepository.UsernameExists(user.Username))
        {
            ModelState.AddModelError("Username", "Username is already used.");
        }

        if (!ModelState.IsValid)
        {
            return View(user);
        }

        _userRepository.Add(user);
        TempData["Message"] = "Registration successful. Please log in.";
        return RedirectToAction("Login");
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel login)
    {
        if (!ModelState.IsValid)
        {
            return View(login);
        }

        User? user = _userRepository.Find(login.Username, login.Password);
        if (user == null)
        {
            ModelState.AddModelError("", "Invalid username or password.");
            return View(login);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.GivenName, user.FirstName)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

        return RedirectToAction("Index", "LaboratorySession");
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}
