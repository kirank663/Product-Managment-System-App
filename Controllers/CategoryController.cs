using Microsoft.AspNetCore.Mvc;
using ProductManagmentApp.DataContext;
using ProductManagmentApp.Models;
using System.Collections.Generic;

namespace ProductManagmentApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DatabaseAppContext _context;
        public CategoryController(DatabaseAppContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _context.categories;
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.categories.Add(category);
                _context.SaveChanges();
                TempData["SuccessMsg"] = "Category (" + category.CategoryName + ") added successfully.";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int? categoryId)
        {
            var category = _context.categories.Find(categoryId);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.categories.Update(category);
                _context.SaveChanges();
                TempData["SuccessMsg"] = "Category (" + category.CategoryName + ") updated successfully.";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int? categoryId)
        {
            var category = _context.categories.Find(categoryId);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int? categoryId)
        {
            var category = _context.categories.Find(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            _context.categories.Remove(category);
            _context.SaveChanges();
            TempData["SuccessMsg"] = "Category (" + category.CategoryName + ") deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
