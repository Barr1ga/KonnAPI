using KonnAPI.Models;

namespace KonnAPI.Interfaces;

public interface IAddressRepository
{
    Task<bool> SaveChangesAsync();
    Task<IEnumerable<Address>> GetAllAddresses();
    Task<IEnumerable<Address>> GetContactAddresses(int id);
    Task<bool> AddAddress(int contactId, Address address);
    Task<bool> UpdateAddress(int addressId, Address address);
}
