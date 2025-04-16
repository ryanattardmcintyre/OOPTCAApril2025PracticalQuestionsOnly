using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPForTCA
{
    public class ProductDbRepository
    {
        private readonly AppDbContext _context;
        public string RepositoryName => "Db Repository";

        public ProductDbRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

    
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

       
        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

  
        public void UpdateProduct(Product updatedProduct)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == updatedProduct.Id);
            if (product != null)
            {
                product.Name = updatedProduct.Name;
                product.Price = updatedProduct.Price;
            }
        }

        public void RestockLowStockProducts()
        {
            _context.Products.Where(p => p.Stock < 10)
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock + 10
                }).ToList();

            _context.SaveChanges();
        }

        public List<ProductSalesViewModel> GetProductSalesReport()
        {
            var result = _context.Products
                .Select(p => new ProductSalesViewModel
                {
                    ProductName = p.Name,
                    TotalUnitsSold = 0
                })
                .ToList();

            return result;
        }



    }

}
