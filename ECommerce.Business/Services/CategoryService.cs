using AutoMapper;
using Ecommerce.Business.DTOs.Category;
using Ecommerce.Business.Exceptions;
using Ecommerce.Business.Interfaces;
using Ecommerce.Data.Entities.Catalog;
using Ecommerce.Data.Interfaces;
namespace Ecommerce.Business.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }


    public async Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto requestDto)
    {

        var category = _mapper.Map<Category>(requestDto);


        await _categoryRepository.SaveAsync(category);

        return _mapper.Map<CategoryReadDto>(category);
      

    }


    public async Task<CategoryReadDto> GetCategoryByIdAsync(int id)
    {

        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            throw new NotFoundAppException(
                "Category not found.",
                "category_not_found"
            );

        }

        return _mapper.Map<CategoryReadDto>(category);
    }

    public async Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);

    }

    public async Task<CategoryReadDto> UpdateCategoryAsync(int id, CategoryUpdateDto requestDto)
    {
     
        var entity = _mapper.Map<Category>(requestDto);

        var category = await _categoryRepository.UpdateAsync(id, entity);

        if (category is null)
        {
            throw new NotFoundAppException(
               "Category not found.",
               "category_not_found"
           );

        }

        return _mapper.Map<CategoryReadDto>(category);

    }

    public async Task DeleteCategoryAsync(int id)
    {
        if (id <= 0)
        {
            throw new ValidationAppException(
              "Invalid id.",
              "category_invalid_id"
             );
        }


        var deleted = await _categoryRepository.DeleteAsync(id);
        if (!deleted)
        {
            throw new NotFoundAppException(
             "Category not found.",
             "category_not_found");
        }

    }
}
