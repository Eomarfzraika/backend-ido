using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCwithWebAPI.Models
{
    public interface ICategoryRepository
    {
        Task Add(Category category);
        Task Update(Category category);
        Task Delete(string id);
        Task<Category> GetCategory(string id);
        Task<IEnumerable<Category>> GetCategories();
    }
}
