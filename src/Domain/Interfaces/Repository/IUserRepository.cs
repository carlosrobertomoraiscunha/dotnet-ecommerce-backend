using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        public User FindByEmail(string email);

        public bool ExistsByEmail(string email);
    }
}
