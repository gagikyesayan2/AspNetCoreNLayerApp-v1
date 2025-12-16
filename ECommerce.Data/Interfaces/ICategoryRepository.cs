using Ecommerce.Data.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<Category> SaveAsync(Category category);
        public Task<Category> GetByIdAsync(int id);

        public Task<IEnumerable<Category>> GetAllAsync();

        public Task<Category> UpdateAsync(int id,Category updatedCategory);

        public Task<bool> DeleteAsync(int id);

    }
}
