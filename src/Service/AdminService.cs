using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Domain.ViewModels;
using Domain.Exceptions;
using Service.Security;
using System.Collections.Generic;
using Domain.ViewModels.User;

namespace Service
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AdminService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public ICollection<UserOutputViewModel> GetAllUsers()
        {
            var users = _userRepository.List();
            var usersViewModel = new List<UserOutputViewModel>();

            foreach (var user in users)
                usersViewModel.Add(_mapper.Map<User, UserOutputViewModel>(user));

            return usersViewModel;
        }

        public UserOutputViewModel GetUserById(long id)
        {
            var user = _userRepository.FindById(id);

            return _mapper.Map<User, UserOutputViewModel>(user);
        }

        public void RemoveUserById(long id)
        {
            var user = _userRepository.FindById(id);

            _userRepository.Remove(user);
        }

        public void UpdateUserById(long id, UserUpdateViewModel userViewModel)
        {
            var userToUpdate = _userRepository.FindById(id);
            var userUpdated = _mapper.Map<UserUpdateViewModel, User>(userViewModel);

            if (!userUpdated.IsValid())
                throw new EntityInvalidException(userUpdated.ErrorMessages);

            userUpdated.Id = userToUpdate.Id;
            userUpdated.Photo.Id = userToUpdate.Photo.Id;
            userUpdated.PhotoId = userToUpdate.PhotoId;
            userUpdated.Email = userToUpdate.Email;
            userUpdated.Password = PasswordEncoder.EncodePasswordToBase64(userUpdated.Password);

            _userRepository.Update(userUpdated);
        }
    }
}
