using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.DTOs;
using ShopAPI.Services.Base;

namespace ShopAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressesController : ApiControllerBase
{
    private readonly IAddressService _addressService;

    public AddressesController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddressViewDto>>> GetAllAsync()
    {
        try
        {
            var addresses = await _addressService.GetAllAddressesAsync();
            return Ok(addresses);
        }
        catch (Exception ex)
        {
            return HandleException<IEnumerable<AddressViewDto>>(ex);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AddressViewDto>> GetByIdAsync(int id)
    {
        try
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            return Ok(address);
        }
        catch (Exception ex)
        {
            return HandleException<AddressViewDto>(ex);
        }
    }

    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<IEnumerable<AddressViewDto>>> GetByUserAsync(int userId)
    {
        try
        {
            var addresses = await _addressService.GetAddressesByUserIdAsync(userId);
            return Ok(addresses);
        }
        catch (Exception ex)
        {
            return HandleException<IEnumerable<AddressViewDto>>(ex);
        }
    }

    [HttpPost("user/{userId:int}")]
    public async Task<ActionResult<AddressViewDto>> CreateAsync(int userId, AddressCreateDto dto)
    {
        try
        {
            var address = await _addressService.CreateAddressAsync(userId, dto);
            return Ok(address);
        }
        catch (Exception ex)
        {
            return HandleException<AddressViewDto>(ex);
        }
    }

    [HttpPut("user/{userId:int}")]
    public async Task<ActionResult<AddressViewDto>> UpdateAsync(int userId, AddressUpdateDto dto)
    {
        try
        {
            var address = await _addressService.UpdateAddressAsync(userId, dto);
            return Ok(address);
        }
        catch (Exception ex)
        {
            return HandleException<AddressViewDto>(ex);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            var deleted = await _addressService.DeleteAddressAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}

