using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.DTOs;
using ShopAPI.Services.Base;

namespace ShopAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ApiControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserViewDto>>> GetAllAsync()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return HandleException<IEnumerable<UserViewDto>>(ex);
        }
    }

    [HttpGet("{id:int}", Name = "GetUserById")]
    public async Task<ActionResult<UserViewDto>> GetByIdAsync(int id)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return HandleException<UserViewDto>(ex);
        }
    }

    [HttpPost]
    public async Task<ActionResult<UserViewDto>> CreateAsync(UserCreateDto dto)
    {
        try
        {
            var user = await _userService.CreateUserAsync(dto);
            return CreatedAtRoute("GetUserById", new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            return HandleException<UserViewDto>(ex);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<UserViewDto>> UpdateAsync(int id, UserUpdateDto dto)
    {
        try
        {
            var user = await _userService.UpdateUserAsync(id, dto);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return HandleException<UserViewDto>(ex);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            var deleted = await _userService.DeleteUserAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}

