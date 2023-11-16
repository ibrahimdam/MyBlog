using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBlog.BusinessLayer;
using MyBlog.Entities.Concrete;

namespace MyBlog.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        CategoryManager _categoryManager = new CategoryManager();

        // GET: Category
        public IActionResult Index()
        {
              return View(_categoryManager.List());
        }

        // GET: Category/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryManager.Find(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
           //Sessiondan kullanıcı ismini alıyoruz ve ModifiedUserName'e veriyoruz.
            string currentUserJson = HttpContext.Session.GetString("currentUser");
            BlogUser currentUser = JsonSerializer.Deserialize<BlogUser>(currentUserJson);
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                _categoryManager.InsertCategory(category, currentUser.UserName);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryManager.GetById(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {            
            if (ModelState.IsValid)
            {
               
                 _categoryManager.Update(category);  
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryManager.Find(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Category category = _categoryManager.GetById(id);
            if (category != null)
            {
                _categoryManager.Delete(category);
            }
            
            return RedirectToAction("Index");
        }

    }
}
