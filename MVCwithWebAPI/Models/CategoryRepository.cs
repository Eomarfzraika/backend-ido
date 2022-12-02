using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVCwithWebAPI.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlDbContext db = new SqlDbContext();
        public async Task Add(Category category)
        {
            category.category_id = Guid.NewGuid().ToString();
            db.Categories.Add(category);
            try
            {
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<Category> GetCategory(string id)
        {
            try
            {
                Category category = await db.Categories.FindAsync(id);
                if (category == null)
                {
                    return null;
                }
                return category;
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
            try
            {
                var categories = await db.Categories.ToListAsync();
                return categories.AsQueryable();
            }
            catch
            {
                throw;
            }
        }
        public async Task Update(Category category)
        {
            try
            {
                db.Entry(category).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task Delete(string id)
        {
            try
            {
                Category category = await db.Categories.FindAsync(id);
                db.Categories.Remove(category);
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private bool CategoryExists(string id)
        {
            return db.Categories.Count(e => e.category_id == id) > 0;
        }

       
    }
}