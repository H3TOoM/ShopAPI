using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.DTOs;
using ShopAPI.Services.Base;

namespace ShopAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartsController : ApiControllerBase
{
    private readonly ICartService _cartService;

    public CartsController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{userId:int}")]
    public async Task<ActionResult<CartItemViewDto>> GetByUserAsync(int userId)
    {
        try
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }
        catch (Exception ex)
        {
            return HandleException<CartItemViewDto>(ex);
        }
    }

    [HttpPost("{userId:int}")]
    public async Task<ActionResult<CartItemViewDto>> CreateAsync(int userId)
    {
        try
        {
            var cart = await _cartService.CreateCart(userId);
            return Ok(cart);
        }
        catch (Exception ex)
        {
            return HandleException<CartItemViewDto>(ex);
        }
    }

    [HttpPut("{userId:int}")]
    public async Task<ActionResult<CartItemViewDto>> UpdateAsync(int userId, CartItemUpdateDto dto)
    {
        try
        {
            var cart = await _cartService.UpdateCart(userId, dto);
            return Ok(cart);
        }
        catch (Exception ex)
        {
            return HandleException<CartItemViewDto>(ex);
        }
    }

    [HttpDelete("{userId:int}")]
    public async Task<ActionResult> ClearAsync(int userId)
    {
        try
        {
            var cleared = await _cartService.ClearCart(userId);
            return cleared ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}

