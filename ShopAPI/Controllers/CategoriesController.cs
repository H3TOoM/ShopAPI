using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.DTOs;
using ShopAPI.Services.Base;

namespace ShopAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ApiControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryViewDto>>> GetAllAsync()
    {
        try
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return HandleException<IEnumerable<CategoryViewDto>>(ex);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryViewDto>> GetByIdAsync(int id)
    {
        try
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }
        catch (Exception ex)
        {
            return HandleException<CategoryViewDto>(ex);
        }
    }

    [HttpPost]
    public async Task<ActionResult<CategoryViewDto>> CreateAsync(CategoryCreateDto dto)
    {
        try
        {
            var category = await _categoryService.CreateCategoryAsync(dto);
            return Ok(category);
        }
        catch (Exception ex)
        {
            return HandleException<CategoryViewDto>(ex);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoryViewDto>> UpdateAsync(int id, CategoryUpdateDto dto)
    {
        try
        {
            var category = await _categoryService.UpdateCategoryAsync(id, dto);
            return Ok(category);
        }
        catch (Exception ex)
        {
            return HandleException<CategoryViewDto>(ex);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            var deleted = await _categoryService.DeleteCategoryAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}

