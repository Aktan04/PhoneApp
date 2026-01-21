using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneApp.Application.DTOs.User;
using PhoneApp.Application.Interfaces;
using PhoneApp.WebApi.Extensions;

namespace PhoneApp.WebApi.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _userService.GetAllAsync());
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
        => Ok(await _userService.GetByIdAsync(id));

    [Authorize]
    [HttpGet("me")]
    public Task<UserDto> GetMe()
    {
        var userId = User.GetUserId();
        return _userService.GetByIdAsync(userId);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
        => Ok(await _userService.CreateAsync(dto));
    
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<UserDto> Update(int id, UpdateUserDto dto)
    {
        var currentUserId = User.GetUserId();

        if (!User.IsAdmin() && currentUserId != id)
            throw new UnauthorizedAccessException("You are not an administrator");

        return await _userService.UpdateAsync(id, dto);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }
}