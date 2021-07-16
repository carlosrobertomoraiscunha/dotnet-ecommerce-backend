using Domain.ViewModels.Address;
using System.Collections.Generic;

namespace Domain.Interfaces.Service
{
    public interface IAddressService
    {
        public void CreateAddressForLoggedUser(string token, AddressViewModel addressViewModel);
        public ICollection<AddressOutputViewModel> GetAllAddressesFromLoggedUser(string token);
        public AddressOutputViewModel GetAddressByIdFromLoggedUser(string token, long id);
        public void RemoveAddressByIdFromLoggedUser(string token, long id);
        public void UpdateAddressByIdFromLoggedUser(string token,
                                                    long id,
                                                    AddressViewModel addressViewModel);
    }
}
