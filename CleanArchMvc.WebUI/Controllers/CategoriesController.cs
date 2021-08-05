using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers {
    public class CategoriesController : Controller {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service) {
            _service = service;
        }
        [HttpGet()]
        public async Task<IActionResult> Index() {
            var categories = await _service.GetCategories();
            return View(categories);
        }
        [HttpGet()]
        public IActionResult Create() {
            return View();
        }
        [HttpPost()]
        public async Task<IActionResult> Create(CategoryDTO category) {
            if (ModelState.IsValid) {
                await _service.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        [HttpGet()]
        public async Task< IActionResult> Edit(int? id) {
            if(id == null ) {
                return NotFound();
            }
            var category = await _service.GetById(id.Value);
            if(category == null) {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost()]
        public async Task<IActionResult> Edit(CategoryDTO category) {
            if (ModelState.IsValid) {
                try {
                    await _service.Update(category);
                } catch(ArgumentException e) {
                    return BadRequest(e);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
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
            var category = await _service.GetById(id.Value);
            if (category == null) {
                return NotFound();
            }
            return View(category);
        }
    }
}
