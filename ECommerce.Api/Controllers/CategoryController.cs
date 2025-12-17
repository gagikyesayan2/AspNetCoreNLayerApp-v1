
using Ecommerce.Api.Models.Category;
using Ecommerce.Business.DTOs.Category;
using Ecommerce.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateModel requestModel)
    {

        var dto = new CategoryCreateDto
        {
            Name = requestModel.Name,
            Description = requestModel.Description
        };

        var result = await _categoryService.CreateCategoryAsync(dto);

        var responseModel = new CategoryReadModel
        {
            Id = result.Id,
            Name = result.Name,
            Description = result.Description
        };
        return CreatedAtAction(
            nameof(GetCategory),
            new { id = responseModel.Id },
            responseModel
            );
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryReadModel>> GetCategory(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
 

        var model = new CategoryReadModel
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
        return Ok(model);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await _categoryService.GetAllCategoriesAsync();

        var model = result.Select(d => new CategoryReadModel
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description
        });

        return Ok(model);
    }

    [HttpPost("{id:int}")]
    public async Task<IActionResult> UpdateCategory(int id, CategoryUpdateModel model)
    {
        if (model == null || id <= 0)
        {
            return BadRequest("Invalid id or data");
        }
        var dto = new CategoryReadDto
        {
            Name = model.Name,
            Description = model.Description
        };
        var category = await _categoryService.UpdateCategoryAsync(id, dto);

        return Ok(new CategoryUpdateModel
        {
            Name = category.Name,
            Description = category.Description,
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        if (id <= 0)
        {
            return BadRequest("invalid id");
        }
        await _categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }
}
