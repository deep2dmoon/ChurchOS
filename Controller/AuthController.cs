using htmos.contract;
using htmos.dtos;
using htmos.model;
using htmos.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using producer.model;
namespace htmos.controller;

[ApiController]
[Route("api/")]
public class AuthController(AuthService authService, IUserRepository userRepository, SmsService smsService) : ControllerBase
{
    [HttpPost("member/new")]
    [EnableRateLimiting("member/new")]
    public async Task<IActionResult> Register(NewUser user)
    {
        var result = await userRepository.RegisterUserAsync(user);
        object response = await smsService.SendSMS();
        return Ok(response);
    }

    [HttpPost("member/login")]
    [EnableRateLimiting("member/login")]
    public async Task<IActionResult> Login(UserDTO user)
    {
        var user_ = await authService.GetUserAsync(user)!;
        if (user_ != null)
        {
            string token = await authService.GetTokenAsync(user_);
            var response = new { response = "user account logged in successfully", token };
            return Ok(response);
        }
        return Ok(new { response = "Invalid login details" });
    }

    [HttpGet("member/all")]
    [Authorize(Policy = "CanWrite")]
    public async Task<IActionResult> GetMembersAsync()
    {
        List<User> users = await userRepository.GetUsersAsync();
        return Ok(users);
    }

    [HttpPost("branch/admin/new")]
    [Authorize(Policy = "CanWriteBranch")]
    public async Task<IActionResult> RegisterBranchAdmin(NewBranchDto admin)
    {
        var result = await userRepository.RegisterBranchAdmin(admin);
        return Ok(result);
    }


    [HttpDelete("member/delete/{id}")]
    [Authorize(Policy = "CanWrite")]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        bool IsDeleted = await userRepository.DeleteUserAsync(id);
        return IsDeleted ? NoContent() : NotFound();
    }
}