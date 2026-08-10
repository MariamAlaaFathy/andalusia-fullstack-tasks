using FullStackSession6.Model;
using FullStackSession6.Repositories.Interfaces;
using FullStackSession6.Services.Interfaces;

namespace FullStackSession6.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _repo;
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public List<Product> GetProducts()
        {
            return _repo.GetProducts();
        }

        public Product GetProductById(int id)
        {
            return _repo.GetProductById(id);
        }

        public Product CreateProduct(Product product)
        {
            return _repo.CreateProduct(product);
        }

        public Product UpdateProduct(int id, Product product)
        {
            return _repo.UpdateProduct(id, product);
        }

        public void DeleteProduct(int id)
        {
            _repo.DeleteProduct(id);
        }

        public void UpdateProductName(int id, String name)
        {
            _repo.UpdateProductName(id, name);
        }
    }
}
