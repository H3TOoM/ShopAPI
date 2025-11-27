using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.DTOs;
using ShopAPI.Services.Base;

namespace ShopAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ApiControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderViewDto>>> GetAllAsync()
    {
        try
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return HandleException<IEnumerable<OrderViewDto>>(ex);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderViewDto>> GetByIdAsync(int id)
    {
        try
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }
        catch (Exception ex)
        {
            return HandleException<OrderViewDto>(ex);
        }
    }

    [HttpPost]
    public async Task<ActionResult<OrderViewDto>> CreateAsync(OrderCreateDto dto)
    {
        try
        {
            var order = await _orderService.CreateOrderAsync(dto);
            return Ok(order);
        }
        catch (Exception ex)
        {
            return HandleException<OrderViewDto>(ex);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<OrderViewDto>> UpdateAsync(int id, OrderUpdateDto dto)
    {
        try
        {
            var order = await _orderService.UpdateOrderAsync(id, dto);
            return Ok(order);
        }
        catch (Exception ex)
        {
            return HandleException<OrderViewDto>(ex);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            var deleted = await _orderService.DeleteOrderAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}

