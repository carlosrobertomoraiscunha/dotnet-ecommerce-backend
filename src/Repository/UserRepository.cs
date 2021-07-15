using Domain.Entities;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EcommerceDbContext _ecommerceDbContext;

        public UserRepository(EcommerceDbContext ecommerceDbContext)
        {
            _ecommerceDbContext = ecommerceDbContext;
        }

        public bool ExistsByEmail(string email)
        {
            return _ecommerceDbContext.Users.Any(u => u.Email == email);
        }

        public User FindByEmail(string email)
        {
            var user = _ecommerceDbContext.Users.Include(u => u.Photo)
                                                .FirstOrDefault(u => u.Email == email);

            return user;
        }

        public User FindById(long id)
        {
            var user = _ecommerceDbContext.Users.Include(u => u.Photo)
                                            .FirstOrDefault(u => u.Id == id);

            if(user == null)
                throw new EntityNotFoundException($"Usuário com id {id} não encontrado");

            return user;
        }

        public ICollection<User> List()
        {
            return _ecommerceDbContext.Users.Include(u => u.Photo)
                                            .ToList();
        }

        public void Remove(User user)
        {
            _ecommerceDbContext.Users.Remove(user);
            _ecommerceDbContext.Images.Remove(user.Photo);
            _ecommerceDbContext.SaveChanges();
        }

        public void Save(User user)
        {
            _ecommerceDbContext.Users.Add(user);
            _ecommerceDbContext.SaveChanges();
        }

        public void Update(User user)
        {
            _ecommerceDbContext.Entry(_ecommerceDbContext.Images.First(i => i.Id == user.Photo.Id)).CurrentValues
                               .SetValues(user.Photo);
            _ecommerceDbContext.Entry(_ecommerceDbContext.Users.First(u => u.Id == user.Id)).CurrentValues
                               .SetValues(user);
            _ecommerceDbContext.SaveChanges();
        }
    }
}
