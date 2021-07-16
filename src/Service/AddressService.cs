using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Domain.ViewModels.Address;
using EcommerceAPI.Service.Security;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Service
{
    public class AddressService : IAddressService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository addressRepository,
                              IMapper mapper,
                              IUserRepository userRepository)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public void CreateAddressForLoggedUser(string token, AddressViewModel addressViewModel)
        {
            var user = GetUserByToken(token);
            var address = _mapper.Map<AddressViewModel, Address>(addressViewModel);

            address.User = user;

            if (!address.IsValid())
                throw new EntityInvalidException(address.ErrorMessages);

            _addressRepository.Save(address);
        }

        public AddressOutputViewModel GetAddressByIdFromLoggedUser(string token, long id)
        {
            var user = GetUserByToken(token);
            var address = _addressRepository.FindById(id);

            if (address.UserId != user.Id)
                throw new EntityNotFoundException($"O endereço com id {id} não foi encontrado no usuário atualmente logado");

            return _mapper.Map<Address, AddressOutputViewModel>(address);
        }

        public ICollection<AddressOutputViewModel> GetAllAddressesFromLoggedUser(string token)
        {
            var user = GetUserByToken(token);
            var addresses = _addressRepository.List().Where(a => a.UserId == user.Id);
            var addressesViewModel = new List<AddressOutputViewModel>();

            foreach (var address in addresses)
                addressesViewModel.Add(_mapper.Map<Address, AddressOutputViewModel>(address));

            return addressesViewModel;
        }

        public void RemoveAddressByIdFromLoggedUser(string token, long id)
        {
            var user = GetUserByToken(token);
            var address = _addressRepository.FindById(id);

            if (address.UserId != user.Id)
                throw new EntityNotFoundException($"O endereço com id {id} não foi encontrado no usuário atualmente logado");

            _addressRepository.Remove(address);
        }

        public void UpdateAddressByIdFromLoggedUser(string token, long id, AddressViewModel addressViewModel)
        {
            var user = GetUserByToken(token);
            var addressToUpdate = _addressRepository.FindById(id);
            var addressUpdated = _mapper.Map<AddressViewModel, Address>(addressViewModel);

            if (addressToUpdate.UserId != user.Id)
                throw new EntityNotFoundException($"O endereço com id {id} não foi encontrado no usuário atualmente logado");

            addressUpdated.Id = addressToUpdate.Id;
            addressUpdated.User = addressToUpdate.User;
            addressUpdated.UserId = addressToUpdate.UserId;

            if (!addressUpdated.IsValid())
                throw new EntityInvalidException(addressUpdated.ErrorMessages);

            _addressRepository.Update(addressUpdated);
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
