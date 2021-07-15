using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceDbContext _ecommerceDbContext;

        public ProductRepository(EcommerceDbContext ecommerceDbContext)
        {
            _ecommerceDbContext = ecommerceDbContext;
        }

        public Product FindById(long id)
        {
            var product = _ecommerceDbContext.Products.Include(p => p.Picture)
                                                      .FirstOrDefault(p => p.Id == id);

            if (product == null)
                throw new EntityNotFoundException($"Produto com id {id} não encontrado");

            return product;
        }

        public ICollection<Product> List()
        {
            return _ecommerceDbContext.Products.Include(p => p.Picture)
                                               .ToList();
        }

        public void Remove(Product product)
        {
            _ecommerceDbContext.Products.Remove(product);
            _ecommerceDbContext.Images.Remove(product.Picture);
            _ecommerceDbContext.SaveChanges();
        }

        public void Save(Product product)
        {
            _ecommerceDbContext.Products.Add(product);
            _ecommerceDbContext.SaveChanges();
        }

        public void Update(Product product)
        {
            _ecommerceDbContext.Entry(_ecommerceDbContext.Images.First(i => i.Id == product.Picture.Id)).CurrentValues
                               .SetValues(product.Picture);
            _ecommerceDbContext.Entry(_ecommerceDbContext.Products.First(p => p.Id == product.Id)).CurrentValues
                               .SetValues(product);
            _ecommerceDbContext.SaveChanges();
        }
    }
}
