using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPForTCA
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;

    public class ProductFileRepository
    {
        private readonly string _filePath;
        public string RepositoryName => "File Repository";
        public ProductFileRepository(string path)
        {
            _filePath = path;
        }

        public void AddProduct(Product product)
        {
            List<Product> products = GetProducts(); 
            products.Add(product); 

            string json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json); 
        }

        public List<Product> GetProducts()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Product>();
            }

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }
    }


}
