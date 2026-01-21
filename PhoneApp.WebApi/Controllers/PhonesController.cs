using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneApp.Application.DTOs.Phone;
using PhoneApp.Application.Interfaces;
using PhoneApp.WebApi.Extensions;

namespace PhoneApp.WebApi.Controllers;

[ApiController]
[Route("api/phones")]
[Authorize]
public class PhonesController : ControllerBase
{
    private readonly IPhoneService _phoneService;

    public PhonesController(IPhoneService phoneService)
    {
        _phoneService = phoneService;
    }
    
    [HttpGet]
    public async Task<IEnumerable<PhoneDto>> GetMyPhones()
    {
        var userId = User.GetUserId();
        return await _phoneService.GetByUserIdAsync(userId);
    }
    
    [HttpPost]
    public async Task<PhoneDto> CreateMyPhone(CreatePhoneDto dto)
    {
        var userId = User.GetUserId();
        return await _phoneService.CreateAsync(userId, dto);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdatePhoneDto dto)
    {
        var phone = await _phoneService.GetEntityByIdAsync(id);
        var currentUserId = User.GetUserId();

        if (!User.IsAdmin() && phone.UserId != currentUserId)
            throw new UnauthorizedAccessException("Forbidden");

        return Ok(await _phoneService.UpdateAsync(id, dto));
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var phone = await _phoneService.GetEntityByIdAsync(id);
        var currentUserId = User.GetUserId();

        if (!User.IsAdmin() && phone.UserId != currentUserId)
            throw new UnauthorizedAccessException("Forbidden");

        await _phoneService.DeleteAsync(id);
        return NoContent();
    }
}