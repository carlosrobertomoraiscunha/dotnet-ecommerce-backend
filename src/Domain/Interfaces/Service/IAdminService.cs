using Domain.ViewModels;
using Domain.ViewModels.User;
using System.Collections.Generic;

namespace Domain.Interfaces.Service
{
    public interface IAdminService
    {
        public ICollection<UserOutputViewModel> GetAllUsers();
        public UserOutputViewModel GetUserById(long id);
        public void RemoveUserById(long id);
        public void UpdateUserById(long id, UserUpdateViewModel userViewModel);
    }
}
