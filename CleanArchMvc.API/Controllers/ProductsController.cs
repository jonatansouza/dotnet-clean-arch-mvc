using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase {
        private readonly IProductService _service;

        public ProductsController(IProductService service) {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get() {
            var products = await _service.GetProducts();
            return Ok(products);
        }
        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<IActionResult> Get(int id) {
            var product = await _service.GetById(id);
            if(product == null) {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Post(ProductDTO product) {
            if(!ModelState.IsValid) {
                return BadRequest();
            }
            try {
                await _service.Add(product);
                return Created(new Uri(Url.Link("GetProductById", new { id = product.Id })), product);
            } catch(ArgumentException e) {
                return BadRequest(e);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, ProductDTO productDTO) {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            var product = await _service.GetById(id);
            if (product == null) {
                return NotFound();
            }
            try {
                await _service.Update(productDTO);
                return Ok(productDTO);
            } catch (ArgumentException e) {
                return BadRequest(e);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) {
            var product = await _service.GetById(id);
            if (product == null) {
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
