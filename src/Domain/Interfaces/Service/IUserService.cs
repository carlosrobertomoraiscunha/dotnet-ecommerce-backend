using Domain.ViewModels;
using Domain.ViewModels.User;

namespace Domain.Interfaces.Service
{
    public interface IUserService
    {
        public UserTokenViewModel SignUp(UserSignUpViewModel userSignUpViewModel);
        public UserTokenViewModel LogIn(string email, string password);
        public UserOutputViewModel GetLoggedUser(string token);
        public void UpdateLoggedUser(string token, UserUpdateViewModel userViewModel);
        public void RemoveLoggedUser(string token);
    }
}
