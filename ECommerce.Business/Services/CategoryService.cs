using Ecommerce.Business.DTOs.Category;
using Ecommerce.Business.Interfaces;
using Ecommerce.Data.Entities.Catalog;
using Ecommerce.Data.Interfaces;
using Microsoft.IdentityModel.Tokens;
namespace Ecommerce.Business.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }


    public async Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto dto)
    {

        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description
        };

        await _categoryRepository.SaveAsync(category);

        return new CategoryReadDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }


    public async Task<CategoryReadDto> GetCategoryByIdAsync(int id)
    {

        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            throw new Exception("category is not found");
        }

        return new CategoryReadDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };

    }
    public async Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
      
        return categories.Select(c => new CategoryReadDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
        });
    }

    public async Task<CategoryReadDto> UpdateCategoryAsync(int id, CategoryReadDto updatedCategory)
    {
        var entity = new Category
        {
            Id = updatedCategory.Id,
            Name = updatedCategory.Name,
            Description = updatedCategory.Description
        };

        var category = await _categoryRepository.UpdateAsync(id, entity);

        if (category is null)
        {
            throw new Exception("category is not found");
        }

        return new CategoryReadDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };


    }
    public async Task DeleteCategoryAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("invalid id");
        }


        var deleted = await _categoryRepository.DeleteAsync(id);
        if (!deleted)
            throw new KeyNotFoundException("Category not found");
    }
}
