using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.DTOs;
using ShopAPI.Services.Base;

namespace ShopAPI.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AccountController : ApiControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserViewDto>> RegisterAsync(UserCreateDto dto)
    {
        try
        {
            var user = await _accountService.RegisterAsync(dto);
            return CreatedAtRoute("GetUserById", new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            return HandleException<UserViewDto>(ex);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> LoginAsync(UserLoginDto dto)
    {
        try
        {
            var response = await _accountService.LoginAsync(dto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException<AuthResponseDto>(ex);
        }
    }
}

