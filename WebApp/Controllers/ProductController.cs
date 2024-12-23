using Domein.Models;
using Infrastructure.Interface;
using Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController(IProduct _product) : ControllerBase
{
    [HttpGet("GetAll")]
    public async Task<Response<List<Product>>> GetAllAsync() => await _product.GetProducts();
    
    [HttpGet("GetById")]
    public async Task<Response<Product>> GetByIdAsync(int id) => await _product.GetProductById(id);
    
    [HttpPost("Add")]
    public async Task<Response<bool>> AddAsync(Product product) => await _product.AddProduct(product);
    
    [HttpPut("Update")]
    public async Task<Response<bool>> UpdateAsync(Product product) => await _product.UpdateProduct(product);
    
    [HttpDelete("Delete")]
    public async Task<Response<bool>> DeleteAsync(int id) => await _product.DeleteProduct(id);
    
    [HttpGet("GetProductToText")]
    public async Task<Response<bool>> AtDbToText()=> await _product.AtDbToText();
    
    [HttpGet("GetTextToProduct")]
    public async Task<Response<bool>> AtTextToDb()=> await _product.AtTextToDb();
}