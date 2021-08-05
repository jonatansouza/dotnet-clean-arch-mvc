using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers {
    public class CategoriesController : Controller {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service) {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Index() {
            var categories = await _service.GetCategories();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO category) {
            if (ModelState.IsValid) {
                await _service.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}
