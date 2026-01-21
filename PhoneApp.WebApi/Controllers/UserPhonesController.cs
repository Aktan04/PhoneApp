using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneApp.Application.DTOs.Phone;
using PhoneApp.Application.Interfaces;
using PhoneApp.WebApi.Extensions;

namespace PhoneApp.WebApi.Controllers;

[ApiController]
[Route("api/users/{userId:int}/phones")]
[Authorize(Roles = "Admin")]
public class UserPhonesController : ControllerBase
{
    private readonly IPhoneService _phoneService;

    public UserPhonesController(IPhoneService phoneService)
    {
        _phoneService = phoneService;
    }

    [HttpGet]
    public async Task<IEnumerable<PhoneDto>> GetByUser(int userId)
        => await _phoneService.GetByUserIdAsync(userId);

    [HttpPost]
    public async Task<PhoneDto> Create(int userId, CreatePhoneDto dto)
        => await _phoneService.CreateAsync(userId, dto);
}