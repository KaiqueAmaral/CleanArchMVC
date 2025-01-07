using CleanArchMVC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMVC.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetByIdAsync(int? id);
        Task<Product> GetByCategoryIdAsync(int? id);

        Task<Product> CreateAsync(Product Product);
        Task<Product> RemoveAsync(Product Product);
        Task<Product> UpdateAsync(Product Product);
    }
}
