
using AutoMapper;
using Ecommerce.Api.Models.Category;
using Ecommerce.Business.DTOs.Category;
using Ecommerce.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CategoryController(ICategoryService categoryService, IMapper mapper) : ControllerBase
{
   
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateModel requestModel)
    {

        var requestDto = mapper.Map<CategoryCreateDto>(requestModel);

        var responseDto = await categoryService.CreateCategoryAsync(requestDto);

        var responseModel = mapper.Map<CategoryReadModel>(responseDto);

        return CreatedAtAction(
            nameof(GetCategory),
            new { id = responseModel.Id },
            responseModel
            );
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryReadModel>> GetCategory(int id)
    {
        var responseDto = await categoryService.GetCategoryByIdAsync(id);

        var responseModel = mapper.Map<CategoryReadModel>(responseDto);
     
        return Ok(responseModel);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var responseDto = await categoryService.GetAllCategoriesAsync();
        var responseModel = mapper.Map<IEnumerable<CategoryReadModel>>(responseDto);

        return Ok(responseModel);
    }

    [HttpPost("{id:int}")]
    public async Task<IActionResult> UpdateCategory(int id, CategoryUpdateModel requestModel)
    {
        if (requestModel == null || id <= 0)
        {
            return BadRequest("Invalid id or data");
        }
   
        var requestDto = mapper.Map<CategoryUpdateDto>(requestModel);

        var responseDto = await categoryService.UpdateCategoryAsync(id, requestDto);

        var responseModel = mapper.Map<CategoryUpdateModel>(responseDto);

        return Ok(responseModel);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        if (id <= 0)
        {
            return BadRequest("invalid id");
        }
        await categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }
}
