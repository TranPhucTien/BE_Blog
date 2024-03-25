using Blog.Core.Services;
using Blog.Models.DTOs.Account;
using Blog.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(
                x => x.UserName == loginDto.UserName);

            if (user == null)
            {
                return Unauthorized("Not found user");
            }

            var passwordCheck = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!passwordCheck.Succeeded)
            {
                return Unauthorized("Invalid password");
            }

            return Ok(
                new NewUserDto
                {
                    Id = user.Id,
                    UserName = user.UserName!,
                    Email = user.Email!,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };

            var createdUser = await _userManager.CreateAsync(user, registerDto.Password);

            if (!createdUser.Succeeded)
            {
                return StatusCode(500, createdUser.Errors);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "User");

            if (!roleResult.Succeeded)
            {
                return StatusCode(500, roleResult.Errors);
            }

            return Ok(
                new NewUserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }
    
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        try
        {
            await _signInManager.SignOutAsync();
            return Ok("logout success");
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }
}