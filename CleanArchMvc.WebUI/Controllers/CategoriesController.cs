using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers {
    public class CategoriesController : Controller {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service) {
            _service = service;
        }
        [HttpGet]
        public async  Task<IActionResult> Index() {
            var categories = await _service.GetCategories();
            return View(categories);
        }
    }
}
