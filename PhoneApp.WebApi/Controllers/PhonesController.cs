using Microsoft.AspNetCore.Mvc;
using PhoneApp.Application.DTOs.Phone;
using PhoneApp.Application.Interfaces;
using PhoneApp.WebApi.Extensions;

namespace PhoneApp.WebApi.Controllers;

public class PhonesController : ControllerBase
{
    
    private readonly IPhoneService _phoneService;

    public PhonesController(IPhoneService phoneService)
    {
        _phoneService = phoneService;
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdatePhoneDto dto)
    {
        var phone = await _phoneService.GetEntityByIdAsync(id);
        var userId = User.GetUserId();

        if (!User.IsAdmin() && phone.UserId != userId)
            throw new UnauthorizedAccessException("You are not an administrator");

        return Ok(await _phoneService.UpdateAsync(id, dto));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var phone = await _phoneService.GetEntityByIdAsync(id);
        var userId = User.GetUserId();

        if (!User.IsAdmin() && phone.UserId != userId)
            throw new UnauthorizedAccessException("You are not an administrator");
        await _phoneService.DeleteAsync(id);
        return NoContent();
    }
}