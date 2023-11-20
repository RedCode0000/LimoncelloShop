using LimoncelloShop.Api.Models;
using LimoncelloShop.Business.Extensions;
using LimoncelloShop.Domain.Interfaces;
using LimoncelloShop.Domain.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ComputerRepairShop.Api.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _config;

    public AuthController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IEmailService emailService, IConfiguration config)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _emailService = emailService;
        _config = config;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (model == null)
        {
            return BadRequest();
        }

        User? user = await _userManager.FindByEmailAsync(model.Email!);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password!))
        {
            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Contains(UserRoles.User) && !await _userManager.IsEmailConfirmedAsync(user))
                return Unauthorized();

            List<Claim> claims = new()
                {
                    new Claim(ClaimTypes.Name, user.UserName!)
                };

            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.UserName!));
            await _userManager.AddClaimsAsync(user, userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
            SymmetricSecurityKey authSigningKey = new(Encoding.UTF8.GetBytes(_config["JWT:Secret"]!));

            JwtSecurityToken token = new(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                user.UserName,
                user.FirstName,
                user.LastName,
                role = userRoles.First(),
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        return BadRequest();
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        ValidateUser(model);

        User? userExists = await _userManager.FindByEmailAsync(model.Email!);
        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status409Conflict, "Email already exists");
        }

        User user = new()
        {
            Email = model.Email,
            UserName = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            FirstName = model.FirstName,
            MiddleName = model.MiddleName,
            LastName = model.LastName,
            Address = model.Address,
            City = model.City,
            Zipcode = model.Zipcode,
            PhoneNumber = model.PhoneNumber,
        };

        IdentityResult result = await _userManager.CreateAsync(user, model.Password!);
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, string.Join(';', result.Errors.Select(x => x.Description)));
        }
        string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        string? confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = model.Email }, Request.Scheme);
        await _emailService.SendValidationEmailAsync(model.Email, confirmationLink);

        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
        }

        await _userManager.AddToRoleAsync(user, UserRoles.User);

        return Ok();
    }

    //[Authorize(Roles = "Admin")]
    [AllowAnonymous]
    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
    {
        ValidateUser(model);

        User? userExists = await _userManager.FindByEmailAsync(model.Email!);
        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status409Conflict, "Email already exists");
        }

        User user = new()
        {
            Email = model.Email,
            UserName = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            FirstName = model.FirstName,
            MiddleName = model.MiddleName,
            LastName = model.LastName,
            Address = model.Address,
            City = model.City,
            Zipcode = model.Zipcode,
            PhoneNumber = model.PhoneNumber,
        };
        IdentityResult result = await _userManager.CreateAsync(user, model.Password!);
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, string.Join(';', result.Errors.Select(x => x.Description)));
        }

        string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        string? confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = model.Email }, Request.Scheme);
        await _emailService.SendValidationEmailAsync(model.Email, confirmationLink);

        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        }
        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
        }

        await _userManager.AddToRoleAsync(user, UserRoles.Admin);

        return Ok();
    }

    //[Authorize]
    //[HttpGet]
    //[Route("getStaff")]
    //public async Task<IActionResult> GetAllAdminsAndEmployees()
    //{
    //    IList<User> admins = await _userManager.GetUsersInRoleAsync(UserRoles.Admin);
    //    IList<User> employees = await _userManager.GetUsersInRoleAsync(UserRoles.Employee);
    //    return Ok(employees.Concat(admins));
    //}

    //[Authorize]
    //[HttpGet]
    //[Route("getAllUsers")]
    //public async Task<IActionResult> GetAllCustomers()
    //{
    //    IList<User> a = await _userManager.GetUsersInRoleAsync(UserRoles.User);
    //    return Ok(a);
    //}

    private void ValidateUser(RegisterModel model)
    {
        if (!model.Email!.IsValidEmail())
        {
            BadRequest("Email is invalid.");
        }
        if (!model.PhoneNumber!.IsValidNLPhoneNr())
        {
            BadRequest("Password must contain at least 8 characters, one uppercase and one lowercase, a number and a special character.");
        }
        if (!model.Zipcode!.IsValidZipCode())
        {
            BadRequest("Zipcode must contain 4 digits, white space and 2 uppercase letters");
        }
    }
}

