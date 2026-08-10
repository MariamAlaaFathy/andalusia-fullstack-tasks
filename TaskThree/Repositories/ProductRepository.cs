using FullStackSession6.Model;
using FullStackSession6.Repositories.Interfaces;

namespace FullStackSession6.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> _products = new List<Product>();

        public List<Product> GetProducts()
        {
            return _products;
        }

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id)!;
        }

        public Product CreateProduct(Product product) {
            _products.Add(product);
            return product;
        }

        public Product UpdateProduct(int id, Product product)
        {
            Product existingProduct = _products.FirstOrDefault(p => p.Id == id)!;
            if (existingProduct != null)
            {
                if (product.Id == 0) product.Id = id;
                if (product.Name == null) product.Name = existingProduct.Name;
                if (product.Price == 0) product.Price = existingProduct.Price;

                _products.Remove(existingProduct);
                _products.Add(product);
            }
            return product;
        }

        public void DeleteProduct(int id)
        {
            _products.RemoveAll(p => p.Id == id);
        }

        public void UpdateProductName(int id, String name)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.Name = name;
            }
        }
    }
}
