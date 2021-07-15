using Domain.Entities;
using Domain.Interfaces.Repository;
using Repository.Context;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommerceDbContext _ecommerceDbContext;

        public OrderRepository(EcommerceDbContext ecommerceDbContext)
        {
            _ecommerceDbContext = ecommerceDbContext;
        }

        public Order FindById(long id)
        {
            return _ecommerceDbContext.Orders.Find(id);
        }

        public ICollection<Order> List()
        {
            return _ecommerceDbContext.Orders.ToList();
        }

        public void Remove(Order order)
        {
            _ecommerceDbContext.Orders.Remove(order);
            _ecommerceDbContext.SaveChanges();
        }

        public void Save(Order order)
        {
            _ecommerceDbContext.Orders.Add(order);
            _ecommerceDbContext.SaveChanges();
        }

        public void Update(Order order)
        {
            _ecommerceDbContext.Orders.Update(order);
            _ecommerceDbContext.SaveChanges();
        }
    }
}
