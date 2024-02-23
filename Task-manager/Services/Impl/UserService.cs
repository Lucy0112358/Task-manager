using Task_Management.Repository.Interfaces;
using Task_Management.Services.Interfaces;
using Task_Management.Models;
namespace Task_Management.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public string Register(UserDto user)
        {
            return _userRepository.Register(user.Email, user.Password);
        }

        public string SignIn(string email, string password)
        {

            return _userRepository.SignIn(email, password);
        }
    }
}
