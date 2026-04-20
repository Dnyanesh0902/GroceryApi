using GroceryAPI.Data;
using GroceryAPI.DTOs;
using GroceryAPI.Models;
using GroceryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroceryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _services;
        public ProductController(IProductServices services)
        {
            _services = services;
        }
        [HttpGet]
        public async  Task<IActionResult> GetAll()
        {
            var product = await _services.GetAllProduct();
            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _services.GetProductById(id);
            return Ok(product);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDto dto)
        {
            var product = await _services.AddProduct(dto);
            return Ok(product);
        }
        [Authorize(Roles ="Admin,Maintenance")] 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
        {
            var product =await _services.UpdateProduct(id, dto);    
            return Ok(product);
        }
        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _services.DeleteProduct(id);
            if(!product)
                return NotFound();
            return Ok("Product Deleted Successfully.");
        }
    }
}
