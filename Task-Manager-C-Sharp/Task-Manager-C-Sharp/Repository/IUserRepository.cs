using Task_Manager_C_Sharp.Models;

namespace Task_Manager_C_Sharp.Repository
{
    public interface IUserRepository
    {
        public void Save(User user);
        bool UserEmailExists(string email);
        User GetUserbyEmail(string email);
    }
}
