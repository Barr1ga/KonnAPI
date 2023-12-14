using KonnAPI.Models;

namespace KonnAPI.Interfaces;

public interface IAddressRepository {
    Task<IEnumerable<Address>> GetContactAddresses(int id);
}
