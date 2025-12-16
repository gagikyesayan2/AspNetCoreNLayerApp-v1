
using Ecommerce.Api.Models.Category;
using Ecommerce.Business.DTOs.Category;
using Ecommerce.Business.Interfaces;
using Ecommerce.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
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
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateModel model)
        {

            CategoryCreateDto dto = new CategoryCreateDto
            {
                Name = model.Name,
                Description = model.Description
            };

            var createdCategory = await _categoryService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetCategoryById),
                new { id = createdCategory.Id },
                createdCategory
                );
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryReadModel>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

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
            var categories = await _categoryService.GetAllAsync();

            return Ok(categories);
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryUpdateModel model)
        {
            if(model == null || id <= 0)
            {
                return BadRequest("Invalid id or data");
            }
            var dto = new CategoryReadDto
            {
                Name = model.Name,
                Description = model.Description
            };
           var category = await _categoryService.UpdateCategoryAsync(id, dto);

            return Ok(new CategoryUpdateDto
            {
                Name = category.Name,
                Description = category.Description,
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if(id <= 0)
            {
                return BadRequest("invalid id");
            }
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
