using Lamazon.Domain.Entities;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        User GetByEmail(string email);
        User GetById(int id);
        int Insert(User entity);
    }
}
