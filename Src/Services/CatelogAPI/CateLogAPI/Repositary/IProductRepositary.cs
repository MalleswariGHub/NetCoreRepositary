using CateLogAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CateLogAPI.Repositary
{
    public interface IProductRepositary
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetProducts(string Id);
        Task<IEnumerable<Product>> GetProductByName(string ProductName);

        Task<IEnumerable<Product>> GetProductByCategory(string CategoryName);
        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(String Id);



    }
}
