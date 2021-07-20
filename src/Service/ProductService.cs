using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Domain.ViewModels.Product;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public void RegisterProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<ProductViewModel, Product>(productViewModel);

            if (!product.IsValid())
                throw new EntityInvalidException(product.ErrorMessages);

            _productRepository.Save(product);
        }

        public ICollection<ProductOutputViewModel> GetAllProducts()
        {
            var products = _productRepository.List();
            var productsViewModel = new List<ProductOutputViewModel>();

            foreach (var product in products)
                productsViewModel.Add(_mapper.Map<Product, ProductOutputViewModel>(product));

            return productsViewModel;
        }

        public ICollection<ProductOutputViewModel> GetAllProductsByCategory(string category)
        {
            var products = _productRepository.List()
                                             .Where(p => p.Category.ToLower() == category.ToLower());
            var productsViewModel = new List<ProductOutputViewModel>();

            foreach (var product in products)
                productsViewModel.Add(_mapper.Map<Product, ProductOutputViewModel>(product));

            return productsViewModel;
        }

        public ProductOutputViewModel GetProductById(long id)
        {
            var product = _productRepository.FindById(id);
            var productViewModel = _mapper.Map<Product, ProductOutputViewModel>(product);

            return productViewModel;
        }

        public void RemoveProductById(long id)
        {
            var product = _productRepository.FindById(id);

            _productRepository.Remove(product);
        }

        public void UpdateProductById(long id, ProductViewModel productViewModel)
        {
            var productToUpdate = _productRepository.FindById(id);
            var productUpdated = _mapper.Map<ProductViewModel, Product>(productViewModel);

            if(!productUpdated.IsValid())
                throw new EntityInvalidException(productUpdated.ErrorMessages);

            productUpdated.Id = productToUpdate.Id;
            productUpdated.Picture.Id = productToUpdate.Picture.Id;
            productUpdated.PictureId = productToUpdate.PictureId;

            _productRepository.Update(productUpdated);
        }
    }
}
