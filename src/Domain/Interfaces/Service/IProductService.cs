using Domain.ViewModels.Product;
using System.Collections.Generic;

namespace Domain.Interfaces.Service
{
    public interface IProductService
    {
        public void RegisterProduct(ProductViewModel productViewModel);
        public ICollection<ProductOutputViewModel> GetAllProducts();
        public ICollection<ProductOutputViewModel> GetAllProductsByCategory(string category);
        public ProductOutputViewModel GetProductById(long id);
        public void RemoveProductById(long id);
        public void UpdateProductById(long id, ProductViewModel productViewModel);
    }
}
