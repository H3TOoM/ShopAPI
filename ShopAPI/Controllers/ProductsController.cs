using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.DTOs;
using ShopAPI.Services.Base;

namespace ShopAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ApiControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductViewDto>>> GetAllAsync()
    {
        try
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return HandleException<IEnumerable<ProductViewDto>>(ex);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductViewDto>> GetByIdAsync(int id)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }
        catch (Exception ex)
        {
            return HandleException<ProductViewDto>(ex);
        }
    }

    [HttpGet("category/{categoryId:int}")]
    public async Task<ActionResult<IEnumerable<ProductViewDto>>> GetByCategoryAsync(int categoryId)
    {
        try
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }
        catch (Exception ex)
        {
            return HandleException<IEnumerable<ProductViewDto>>(ex);
        }
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<ProductViewDto>>> SearchAsync([FromQuery] string term)
    {
        try
        {
            var products = await _productService.SearchProductsAsync(term);
            return Ok(products);
        }
        catch (Exception ex)
        {
            return HandleException<IEnumerable<ProductViewDto>>(ex);
        }
    }

    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<ProductViewDto>>> FilterByPriceAsync([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
    {
        try
        {
            if (minPrice > maxPrice)
                return BadRequest("Minimum price cannot exceed maximum price.");

            var products = await _productService.FilterByPrice(minPrice, maxPrice);
            return Ok(products);
        }
        catch (Exception ex)
        {
            return HandleException<IEnumerable<ProductViewDto>>(ex);
        }
    }

    [HttpGet("sort/price")]
    public async Task<ActionResult<IEnumerable<ProductViewDto>>> SortByPriceAsync([FromQuery] decimal price = 0)
    {
        try
        {
            var products = await _productService.SortByPrice(price);
            return Ok(products);
        }
        catch (Exception ex)
        {
            return HandleException<IEnumerable<ProductViewDto>>(ex);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ProductViewDto>> CreateAsync(ProductCreateDto dto)
    {
        try
        {
            var product = await _productService.CreateProductAsync(dto);
            return Ok(product);
        }
        catch (Exception ex)
        {
            return HandleException<ProductViewDto>(ex);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductViewDto>> UpdateAsync(int id, ProductUpdateDto dto)
    {
        try
        {
            var product = await _productService.UpdateProductAsync(id, dto);
            return Ok(product);
        }
        catch (Exception ex)
        {
            return HandleException<ProductViewDto>(ex);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            var deleted = await _productService.DeleteProductAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}

