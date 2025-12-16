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
        public Task<IEnumerable<CategoryReadDto>> GetAllAsync();

        public Task<CategoryReadDto> GetByIdAsync(int id);

        public Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto);


        public Task DeleteCategoryAsync(int id);
        public Task<CategoryReadDto> UpdateCategoryAsync(int id, CategoryReadDto updatedCategory);



    }
}
