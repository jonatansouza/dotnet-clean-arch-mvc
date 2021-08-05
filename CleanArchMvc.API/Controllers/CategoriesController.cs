using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service) {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get() {
            var categories = await _service.GetCategories();
            return Ok(categories);
        }
        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public async Task<IActionResult> Get(int id) {
            var category = await _service.GetById(id);
            if (category == null) {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CategoryDTO category) {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            try {
                await _service.Add(category);
                return Created(new Uri(Url.Link("GetCategoryById", new { id = category.Id })), category);
            } catch (ArgumentException e) {
                return BadRequest(e);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, CategoryDTO categoryDTO) {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            var category = await _service.GetById(id);
            if (category == null) {
                return NotFound();
            }
            try {
                await _service.Update(categoryDTO);
                return Ok(categoryDTO);
            } catch (ArgumentException e) {
                return BadRequest(e);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) {
            var category = await _service.GetById(id);
            if (category == null) {
                return NotFound();
            }
            try {
                await _service.Remove(id);
                return NoContent();
            } catch (ArgumentException e) {
                return BadRequest(e);
            }
        }
    }
}
