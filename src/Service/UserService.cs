using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Domain.ViewModels;
using EcommerceAPI.Service.Security;
using Domain.Exceptions;
using Service.Security;
using System;
using System.Security.Claims;
using Domain.ViewModels.User;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserTokenViewModel SignUp(UserSignUpViewModel userSignUpViewModel)
        {
            var user = _mapper.Map<UserSignUpViewModel, User>(userSignUpViewModel);

            if (!user.IsValid())
                throw new EntityInvalidException(user.ErrorMessages);

            if (_userRepository.ExistsByEmail(user.Email))
                throw new EntityAlreadyExistsException("Já existe um usuário com esse email, tente fazer login");

            user.Password = PasswordEncoder.EncodePasswordToBase64(user.Password);
            _userRepository.Save(user);

            var createdUser = _userRepository.FindByEmail(user.Email);
            var loggedUser = _mapper.Map<User, UserTokenViewModel>(createdUser);

            loggedUser.Token = JwtAuthManager.GenerateToken(createdUser);

            return loggedUser;
        }

        public UserTokenViewModel LogIn(string email, string password)
        {
            if (!ValidateUser(email, password))
                throw new UnauthorizedAccessException("As credenciais estão incorretas");

            var user = _userRepository.FindByEmail(email);
            var loggedUser = _mapper.Map<User, UserTokenViewModel>(user);

            loggedUser.Token = JwtAuthManager.GenerateToken(user);

            return loggedUser;
        }

        public UserOutputViewModel GetLoggedUser(string token)
        {
            var user = GetUserByToken(token);

            return _mapper.Map<User, UserOutputViewModel>(user);
        }

        public void UpdateLoggedUser(string token, UserUpdateViewModel userViewModel)
        {
            var userToUpdate = GetUserByToken(token);
            var userUpdated = _mapper.Map<UserUpdateViewModel, User>(userViewModel);

            if (!userUpdated.IsValid())
                throw new EntityInvalidException(userUpdated.ErrorMessages);

            userUpdated.Id = userToUpdate.Id;
            userUpdated.Photo.Id = userToUpdate.Photo.Id;
            userUpdated.PhotoId = userToUpdate.PhotoId;
            userUpdated.Password = PasswordEncoder.EncodePasswordToBase64(userUpdated.Password);

            _userRepository.Update(userUpdated);
        }

        public void RemoveLoggedUser(string token)
        {
            _userRepository.Remove(GetUserByToken(token));
        }

        private bool ValidateUser(string email, string password)
        {
            var user = _userRepository.FindByEmail(email);
            var encodedPassword = PasswordEncoder.EncodePasswordToBase64(password);

            if (user == null)
                return false;
            else if(user.Password == encodedPassword)
                return true;

            return false;
        }

        private User GetUserByToken(string token)
        {
            var (claims, validatedToken) = JwtAuthManager.DecodeJwtToken(token);
            var userEmail = claims.FindFirst(ClaimTypes.Email).Value;
            var user = _userRepository.FindByEmail(userEmail);

            return user;
        }
    }
}
