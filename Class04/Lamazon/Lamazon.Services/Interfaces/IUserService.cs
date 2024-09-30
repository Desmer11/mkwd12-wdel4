using Lamazon.ViewModels.Models;

namespace Lamazon.Services.Interfaces
{
    public interface IUserService
    {
        UserViewModel RegisterUser(RegisterUserViewModel registerUserViewModel);
        UserViewModel GetUserById(int id);
        UserViewModel ValidateUser(UserCredentialsViewModel userCredentialsViewModel);
    }
}
