using FullStackSession6.Model;

namespace FullStackSession6.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public List<Product> GetProducts();
        public Product GetProductById(int id);
        public Product CreateProduct(Product product);
        public Product UpdateProduct(int id, Product product);
        public void DeleteProduct(int id);
        public void UpdateProductName(int id, String name);
    }
}
