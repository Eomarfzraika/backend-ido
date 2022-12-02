using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVCwithWebAPI.Models;

namespace MVCwithWebAPI.Controllers
{
    public class CategoriesController : Controller
    {
        private SqlDbContext db = new SqlDbContext();
        readonly string apiBaseAddress = ConfigurationManager.AppSettings["apiBaseAddress"];
        // GET: Categories
        public async Task<ActionResult> Index()
        {
            IEnumerable<Category> categories = null;
  
            using (var client = new HttpClient())  
            {  
                client.BaseAddress = new Uri(apiBaseAddress);

                 var result = await client.GetAsync("categories/get");  
  
                if (result.IsSuccessStatusCode)  
                {
                    categories = await result.Content.ReadAsAsync<IList<Category>>();
}  
                else  
                {
                    categories = Enumerable.Empty<Category>();  
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");  
                }  
            }  
            return View(categories);  
        }

        // GET: Categories/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var result = await client.GetAsync($"categories/details/{id}");

                if (result.IsSuccessStatusCode)
                {
                    category = await result.Content.ReadAsAsync<Category>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "category_id,name,estimate,importance,due_date,type")] Category category)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseAddress);

                    var response = await client.PostAsJsonAsync("categories/Create", category);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var result = await client.GetAsync($"categories/details/{id}");

                if (result.IsSuccessStatusCode)
                {
                    category = await result.Content.ReadAsAsync<Category>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
         
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "category_id,name,estimate,importance,due_date,type")] Category category)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseAddress);
                    var response = await client.PutAsJsonAsync("categories/edit", category);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }


        // GET: Categories/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var result = await client.GetAsync($"categories/details/{id}");

                if (result.IsSuccessStatusCode)
                {
                    category = await result.Content.ReadAsAsync<Category>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var response = await client.DeleteAsync($"categories/delete/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return View();
        }
 
    }
}
