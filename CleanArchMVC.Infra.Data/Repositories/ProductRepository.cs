using CleanArchMVC.Domain.Entities;
using CleanArchMVC.Domain.Interfaces;
using CleanArchMVC.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMVC.Infra.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _productContext;

    public ProductRepository(ApplicationDbContext context)
    {
        _productContext = context;
    }

    public async Task<Product> CreateAsync(Product Product)
    {
        _productContext.Add(Product);
        await _productContext.SaveChangesAsync();
        return Product;
    }

    public async Task<Product> GetByCategoryIdAsync(int? id)
    {
        //eager loading
        return await _productContext.Products.Include(c => c.Category).SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> GetByIdAsync(int? id)
    {
        return await _productContext.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _productContext.Products.ToListAsync();
    }

    public async Task<Product> RemoveAsync(Product Product)
    {
        _productContext.Remove(Product);
        await _productContext.SaveChangesAsync();
        return Product;
    }

    public async Task<Product> UpdateAsync(Product Product)
    {
        _productContext.Update(Product);
        await _productContext.SaveChangesAsync();
        return Product;
    }
}
