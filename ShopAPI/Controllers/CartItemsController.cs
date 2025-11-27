using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.DTOs;
using ShopAPI.Services.Base;

namespace ShopAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartItemsController : ApiControllerBase
{
    private readonly ICartItemService _cartItemService;

    public CartItemsController(ICartItemService cartItemService)
    {
        _cartItemService = cartItemService;
    }

    [HttpGet("cart/{cartId:int}")]
    public async Task<ActionResult<IEnumerable<CartItemViewDto>>> GetByCartAsync(int cartId)
    {
        try
        {
            var items = await _cartItemService.GetAllCartItemsAsync(cartId);
            return Ok(items);
        }
        catch (Exception ex)
        {
            return HandleException<IEnumerable<CartItemViewDto>>(ex);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CartItemViewDto>> GetByIdAsync(int id)
    {
        try
        {
            var item = await _cartItemService.GetCartItemByIdAsync(id);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException<CartItemViewDto>(ex);
        }
    }

    [HttpPost("cart/{cartId:int}")]
    public async Task<ActionResult<CartItemViewDto>> CreateAsync(int cartId, CartItemCreateDto dto)
    {
        try
        {
            var item = await _cartItemService.CreateCartItemAsync(cartId, dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException<CartItemViewDto>(ex);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CartItemViewDto>> UpdateAsync(int id, CartItemUpdateDto dto)
    {
        try
        {
            var item = await _cartItemService.UpdateCartItemAsync(id, dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException<CartItemViewDto>(ex);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            var deleted = await _cartItemService.DeleteCartItemAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}

