namespace Task_Management.Repository.Interfaces
{
    public interface IUserRepository
    {
        public string Register(string email, string password);
        public string SignIn(string email, string password);
    }
}
