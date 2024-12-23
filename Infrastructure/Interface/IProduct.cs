using Domein.Models;
using Infrastructure.Responses;

namespace Infrastructure.Interface;

public interface IProduct
{
    public Task<Response<List<Product>>> GetProducts();
    public Task<Response<Product>> GetProductById(int id);
    public Task<Response<bool>> AddProduct(Product product);
    public Task<Response<bool>> UpdateProduct(Product product);
    public Task<Response<bool>> DeleteProduct(int id);
    
    public Task<Response<bool>> AtDbToText();
    public Task<Response<bool>> AtTextToDb();
}