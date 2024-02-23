using Task_Management.Models;

namespace Task_Management.Services.Interfaces;

public interface IUserService
{
    public string Register(UserDto user);
    public string SignIn(string email, string password);
}
