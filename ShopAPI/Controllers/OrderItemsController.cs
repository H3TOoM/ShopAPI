using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.DTOs;
using ShopAPI.Services.Base;

namespace ShopAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemsController : ApiControllerBase
{
    private readonly IOrderItemService _orderItemService;

    public OrderItemsController(IOrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    [HttpGet("order/{orderId:int}")]
    public async Task<ActionResult<IEnumerable<OrderItemViewDto>>> GetByOrderAsync(int orderId)
    {
        try
        {
            var items = await _orderItemService.GetAllOrderItemsAsync(orderId);
            return Ok(items);
        }
        catch (Exception ex)
        {
            return HandleException<IEnumerable<OrderItemViewDto>>(ex);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderItemViewDto>> GetByIdAsync(int id)
    {
        try
        {
            var item = await _orderItemService.GetOrderItemByIdAsync(id);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException<OrderItemViewDto>(ex);
        }
    }

    [HttpPost("order/{orderId:int}")]
    public async Task<ActionResult<OrderItemViewDto>> CreateAsync(int orderId, OrderItemCreateDto dto)
    {
        try
        {
            var item = await _orderItemService.CreateOrderItemAsync(orderId, dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException<OrderItemViewDto>(ex);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<OrderItemViewDto>> UpdateAsync(int id, OrderItemUpdateDto dto)
    {
        try
        {
            var item = await _orderItemService.UpdateOrderItemAsync(id, dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException<OrderItemViewDto>(ex);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            var deleted = await _orderItemService.DeleteOrderItemAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}

