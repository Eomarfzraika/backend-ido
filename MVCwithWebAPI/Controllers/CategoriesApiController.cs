using MVCwithWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MVCwithWebAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class CategoriesApiController : ApiController
    {
        private readonly ICategoryRepository _iCategoryRepository = new CategoryRepository();
        SqlDbContext _Context = new SqlDbContext();
        [HttpGet]
        [Route("api/Categories/Get")]
        public async Task<IEnumerable<Category>> Get()
           
        {
            dynamic result = null;
            result = await _iCategoryRepository.GetCategories();
            //return Content(result, "application/json");
            return result;
        }

        [HttpPost]
        [Route("api/Categories/Create")]
        public async Task CreateAsync([FromBody]Category category)
        {
            if (ModelState.IsValid)
            {
                await _iCategoryRepository.Add(category);
            }
        }

        [HttpGet]
        [Route("api/Categories/Details/{id}")]
        public async Task<Category> Details(string id)
        {
            var result = await _iCategoryRepository.GetCategory(id);
           // return Content(result, "application/json");
            return result;
        }

      

        [HttpPost]
        [Route("api/Categories/Edit")]
        public async Task EditAsync([FromBody]Category category)
        {
            if (ModelState.IsValid)
            {
                await _iCategoryRepository.Update(category);
            }
        }

        [HttpPost]
        [Route("api/Categories/Search")]
        public async Task<IEnumerable<Category>> Search([FromBody]Category cat)
        {
            IQueryable<Category> query = _Context.Categories;
            try
            {   if (!string.IsNullOrEmpty((cat.title)))
                    query = query.Where(e => e.name.Contains(cat.title));
                

             
                 
                return await query.ToListAsync();
            }
            catch
            {
                throw;
            }
           
        }

        [HttpDelete]
        [Route("api/Categories/Delete/{id}")]
        public async Task DeleteConfirmedAsync(string id)
        {
            await _iCategoryRepository.Delete(id);
        }
    }
}