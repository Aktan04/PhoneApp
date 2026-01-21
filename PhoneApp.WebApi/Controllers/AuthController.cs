using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneApp.Application.DTOs.Auth;
using PhoneApp.Application.Interfaces;

namespace PhoneApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<TokenResponseDto>> Register(RegisterDto dto)
        => Ok(await _authService.RegisterAsync(dto));

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<TokenResponseDto>> Login(LoginDto dto)
        => Ok(await _authService.LoginAsync(dto));
}