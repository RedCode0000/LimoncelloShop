using LimoncelloShop.Domain.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LimoncelloShop.Api.Controllers;

[Route("Email")]
public class EmailController : Controller
{
    private readonly UserManager<User> _userManager;
    public EmailController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    [Route("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        User? user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return View("Error");

        IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);
        return View(result.Succeeded ? "ConfirmEmail" : "Error");
    }
}
