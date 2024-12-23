using System.Net;
using Dapper;
using Infrastructure.Interface;
using Domein.Models;
using Infrastructure.DataContext;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class ProductService(IContext _context) : IProduct
{
    public async Task<Response<List<Product>>> GetProducts()
    {
        var sql = @"select * from Products";
        var res = await _context.Connection().QueryAsync<Product>(sql);
        return new Response<List<Product>>(res.ToList());
    }

    public async Task<Response<Product>> GetProductById(int id)
    {
        var sql = @"select * from Products where ProductId = @id";
        var res = await _context.Connection().QuerySingleOrDefaultAsync(sql, new { id });
        return new Response<Product>(res);
    }

    public async Task<Response<bool>> AddProduct(Product product)
    {
        var sql = @"insert into Products (Name, Description, Price, StockQuantity, CategoryName, CreatedDate) values (@Name, @Description, @Price, @StockQuantity, @CategoryName, @CreatedDate)";
        var res = await _context.Connection().ExecuteAsync(sql, product);
        return res == 0 
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Product already exists")
            : new Response<bool>(HttpStatusCode.Created, "Success");
    }

    public async Task<Response<bool>> UpdateProduct(Product product)
    {
        var sql = @"update Products set Name=@Name, Description=@Description, Price=@Price, StockQuantity=@StockQuantity, CategoryName=@CategoryName, CreatedDate=@CreatedDate where ProductId = @ProductId";
        var res = await _context.Connection().ExecuteAsync(sql, product);
        return res == 0 
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Product already exists")
            : new Response<bool>(HttpStatusCode.Created, "Success"); 
    }

    public async Task<Response<bool>> DeleteProduct(int id)
    {
        var sql = @"delete from Products where ProductId = @ProductId";
        var res = await _context.Connection().ExecuteAsync(sql, new { ProductId = id });
        return res == 0 
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Product already exists")
            : new Response<bool>(HttpStatusCode.Created, "Success"); 
    }


    public async Task<Response<bool>> AtDbToText()
    {
        IProduct service = new ProductService(_context);
        var ll = service.GetProducts();
        string path =
            "C:\\Users\\ISMOIL\\Desktop\\programing\\C#.2\\API\\Exem23122024\\Infrastructure\\Explorer\\Products.txt";

        List<string> lines = new List<string>();
        foreach (var x in ll.Result.Data)
        {
            lines.Add($"{x.ProductId},{x.Name},{x.Description},{x.Price},{x.StockQuantity},{x.CategoryName},{x.CreatedDate}");
        }
            File.AppendAllLines(path, lines);
        

        return new Response<bool>(HttpStatusCode.OK, "Success");
    }


    public async Task<Response<bool>> AtTextToDb()
    {
        string path =
            "C:\\Users\\ISMOIL\\Desktop\\programing\\C#.2\\API\\Exem23122024\\Infrastructure\\Explorer\\add.txt";
        
        var lines = File.ReadAllLines(path);

        foreach (var x in lines)
        {
            string name;
            string description;
            decimal price;
            int stockQuantity;
            string categoryName;
            DateTime createdDate;
            string[] ll = x.Split(',');
            name = ll[0];
            description = ll[1];
            price = decimal.Parse(ll[2]);
            stockQuantity = int.Parse(ll[3]);
            categoryName = (ll[4]);
            createdDate = DateTime.Parse(ll[5]);

            var prod = new Product();
            prod.Name = name;
            prod.Description = description;
            prod.Price = price;
            prod.StockQuantity = stockQuantity;
            prod.CategoryName = categoryName;
            prod.CreatedDate = createdDate;
            
            IProduct service = new ProductService(_context);
            service.AddProduct(prod);
            
        }
        
        
        return new Response<bool>(HttpStatusCode.OK, "Success");
    }
}