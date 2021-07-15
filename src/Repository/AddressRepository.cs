using Domain.Entities;
using Domain.Interfaces.Repository;
using Repository.Context;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly EcommerceDbContext _ecommerceDbContext;

        public AddressRepository(EcommerceDbContext ecommerceDbContext)
        {
            _ecommerceDbContext = ecommerceDbContext;
        }

        public Address FindById(long id)
        {
            return _ecommerceDbContext.Addresses.Find(id);
        }

        public ICollection<Address> List()
        {
            return _ecommerceDbContext.Addresses.ToList();
        }

        public void Remove(Address address)
        {
            _ecommerceDbContext.Addresses.Remove(address);
            _ecommerceDbContext.SaveChanges();
        }

        public void Save(Address address)
        {
            _ecommerceDbContext.Addresses.Add(address);
            _ecommerceDbContext.SaveChanges();
        }

        public void Update(Address address)
        {
            _ecommerceDbContext.Addresses.Update(address);
            _ecommerceDbContext.SaveChanges();
        }
    }
}
