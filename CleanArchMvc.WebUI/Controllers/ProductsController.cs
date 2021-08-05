using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers {
    public class ProductsController : Controller {
        private readonly IProductService _service;
        private readonly ICategoryService _category;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(IProductService service, ICategoryService category, IWebHostEnvironment environment) {
            _service = service;
            _category = category;
            _environment = environment;
        }
        [HttpGet]
        public async Task<IActionResult> Index() {
            var products = await _service.GetProducts();
            return View(products);
        }
        [HttpGet()]
        public async Task<IActionResult> Create() {
            ViewBag.CategoryId = new SelectList(await _category.GetCategories(), "Id", "Name");
            return View();
        }
        [HttpPost()]
        public async Task<IActionResult> Create(ProductDTO product) {
            if (ModelState.IsValid) {
                await _service.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        [HttpGet()]
        public async Task<IActionResult> Edit(int? id) {
            if(id == null) {
                return NotFound();
            }
            var productDTO = await _service.GetById(id);
            ViewBag.CategoryId = new SelectList(await _category.GetCategories(), "Id", "Name", productDTO.CategoryId);
            return View(productDTO);
        }
        [HttpPost()]
        public async Task<IActionResult> Edit(ProductDTO product) {
            if (ModelState.IsValid) {
                await _service.Update(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }
            var category = await _service.GetById(id.Value);
            if (category == null) {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id) {
            try {
                await _service.Remove(id);
            } catch (ArgumentException e) {
                return BadRequest(e);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet()]
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }
            var product = await _service.GetById(id.Value);
            if (product == null) {
                return NotFound();
            }
            var wwwroot = _environment.WebRootPath;
            var image = Path.Combine(wwwroot, "images\\" + product.Image);
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExist = exists;
            return View(product);
        }

    }
}
