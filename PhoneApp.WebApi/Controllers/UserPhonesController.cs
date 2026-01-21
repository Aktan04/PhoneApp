using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneApp.Application.DTOs.Phone;
using PhoneApp.Application.Interfaces;
using PhoneApp.WebApi.Extensions;

namespace PhoneApp.WebApi.Controllers;

[ApiController]
[Route("api/users/{userId:int}/phones")]
[Authorize]
public class UserPhonesController : ControllerBase
{
    private readonly IPhoneService _phoneService;

    public UserPhonesController(IPhoneService phoneService)
    {
        _phoneService = phoneService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int userId)
        => Ok(await _phoneService.GetByUserIdAsync(userId));
    
    [Authorize]
    [HttpGet("me/phones")]
    public async Task<IEnumerable<PhoneDto>> GetMyPhones()
    {
        var userId = User.GetUserId();
        return await _phoneService.GetByUserIdAsync(userId);
    }
    
    [HttpPost("me/phones")]
    public async Task<IActionResult> Create(int userId, CreatePhoneDto dto)
        => Ok(await _phoneService.CreateAsync(userId, dto));
}