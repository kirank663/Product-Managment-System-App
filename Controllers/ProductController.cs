﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductManagmentApp.DataContext;
using ProductManagmentApp.Models;
using ProductManagmentApp.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProductManagmentApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly DatabaseAppContext _context;
        public ProductController(DatabaseAppContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<ProductListViewModel> productListViewModelList = new List<ProductListViewModel>();
            var productList = _context.products;
            if (productList != null)
            {
                foreach (var item in productList)
                {
                    ProductListViewModel productListViewModel = new ProductListViewModel();
                    productListViewModel.Id = item.Id;
                    productListViewModel.Name = item.Name;
                    productListViewModel.Description = item.Description;
                    productListViewModel.Color = item.Color;
                    productListViewModel.Price = item.Price;
                    productListViewModel.CategoryId = item.CategoryId;
                    productListViewModel.Image = item.Image;
                    productListViewModel.CategoryName = _context.categories.Where(c => c.CategoryId == item.CategoryId).Select(c => c.CategoryName).FirstOrDefault();
                    productListViewModelList.Add(productListViewModel);
                }
            }
            return View(productListViewModelList);
        }

        public IActionResult Create()
        {
            ProductViewModel productCreateViewModel = new ProductViewModel();
            productCreateViewModel.Category = (IEnumerable<SelectListItem>)_context.categories.Select(c => new SelectListItem()
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            });

            return View(productCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel productCreateViewModel)
        {
            productCreateViewModel.Category = (IEnumerable<SelectListItem>)_context.categories.Select(c => new SelectListItem()
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            });
            var product = new Product()
            {
                Name = productCreateViewModel.Name,
                Description = productCreateViewModel.Description,
                Price = productCreateViewModel.Price,
                Color = productCreateViewModel.Color,
                CategoryId = productCreateViewModel.CategoryId,
                Image = productCreateViewModel.Image
            };
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                _context.products.Add(product);
                _context.SaveChanges();
                TempData["SuccessMsg"] = "Product (" + product.Name + ") added successfully.";
                return RedirectToAction("Index");
            }

            return View(productCreateViewModel);
        }

        public IActionResult Edit(int? id)
        {
            var productToEdit = _context.products.Find(id);
            if (productToEdit != null)
            {
                var productViewModel = new ProductViewModel()
                {
                    Id = productToEdit.Id,
                    Name = productToEdit.Name,
                    Description = productToEdit.Description,
                    Price = productToEdit.Price,
                    CategoryId = productToEdit.CategoryId,
                    Color = productToEdit.Color,
                    Image = productToEdit.Image,
                    Category = (IEnumerable<SelectListItem>)_context.categories.Select(c => new SelectListItem()
                    {
                        Text = c.CategoryName,
                        Value = c.CategoryId.ToString()
                    })
                };
                return View(productViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            productViewModel.Category = (IEnumerable<SelectListItem>)_context.categories.Select(c => new SelectListItem()
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            });
            var product = new Product()
            {
                Id = productViewModel.Id,
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                Color = productViewModel.Color,
                CategoryId = productViewModel.CategoryId,
                Image = productViewModel.Image
            };
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                _context.products.Update(product);
                _context.SaveChanges();
                TempData["SuccessMsg"] = "Product (" + product.Name + ") updated successfully !";
                return RedirectToAction("Index");
            }

            return View(productViewModel);
        }
        public IActionResult Delete(int? id)
        {
            var productToEdit = _context.products.Find(id);
            if (productToEdit != null)
            {
                var productViewModel = new ProductViewModel()
                {
                    Id = productToEdit.Id,
                    Name = productToEdit.Name,
                    Description = productToEdit.Description,
                    Price = productToEdit.Price,
                    CategoryId = productToEdit.CategoryId,
                    Color = productToEdit.Color,
                    Image = productToEdit.Image,
                    Category = (IEnumerable<SelectListItem>)_context.categories.Select(c => new SelectListItem()
                    {
                        Text = c.CategoryName,
                        Value = c.CategoryId.ToString()
                    })
                };
                return View(productViewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int? id)
        {
            var product = _context.products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.products.Remove(product);
            _context.SaveChanges();
            TempData["SuccessMsg"] = "Product (" + product.Name + ") deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
