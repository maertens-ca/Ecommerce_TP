using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TravailPratique1;
namespace TravailPratique1;
[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<Models.User> _userManager;
    private readonly SignInManager<Models.User> _signInManager;
    private readonly JwtTokenService _jwtTokenService;

    public AccountController(UserManager<Models.User> userManager, SignInManager<Models.User> signInManager, JwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Models.User model)
    {
        var user = new Models.User { UserName = model.email, Email = model.Email, username = model.username };
        var result = await _userManager.CreateAsync(user, model.password);

        if (result.Succeeded)
        {
            return Ok(new { message = "User registered successfully" });
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Models.User model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null) return Unauthorized();

        var result = await _signInManager.PasswordSignInAsync(user, model.password, false, false);
        if (!result.Succeeded) return Unauthorized();

        var token = await _jwtTokenService.GenerateToken(user);
        return Ok(new { Token = token });
    }
}