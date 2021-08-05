using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers {
    public class ProductsController : Controller {
        private readonly IProductService _service;

        public ProductsController(IProductService service) {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Index() {
            var products = await _service.GetProducts();
            return View(products);
        }
    }
}
