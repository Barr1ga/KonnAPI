using KonnAPI.Models;

namespace KonnAPI.Interfaces;

public interface IAddressRepository {
    Task<IEnumerable<Address>> GetAllAddresses();
    Task<IEnumerable<Address>> GetContactAddresses(int id);
}
