using ECommerce.Core.Entities;

namespace ECommerce.Core.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(string firstName, string lastName, string email, string password);
    }

}
