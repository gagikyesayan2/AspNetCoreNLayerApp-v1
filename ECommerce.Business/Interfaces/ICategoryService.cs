using Ecommerce.Business.DTOs.Category;
using Ecommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync();

        public Task<CategoryReadDto> GetCategoryByIdAsync(int id);

        public Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto dto);


        public Task DeleteCategoryAsync(int id);
        public Task<CategoryReadDto> UpdateCategoryAsync(int id, CategoryReadDto updatedCategory);



    }
}
