using Lamazon.DataAccess.Interfaces;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;

namespace Lamazon.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserViewModel GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public UserViewModel RegisterUser(RegisterUserViewModel registerUserViewModel)
        {
            throw new NotImplementedException();
        }

        public UserViewModel ValidateUser(UserCredentialsViewModel userCredentialsViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
