using Domain.Entities;
using Domain.Exceptions;
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
            var address = _ecommerceDbContext.Addresses.Find(id);

            if (address == null)
                throw new EntityNotFoundException($"Endereço com id {id} não encontrado");

            return address;
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
            _ecommerceDbContext.Entry(_ecommerceDbContext.Addresses.First(a => a.Id == address.Id)).CurrentValues
                               .SetValues(address);
            _ecommerceDbContext.SaveChanges();
        }
    }
}
