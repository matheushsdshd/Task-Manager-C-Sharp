using Task_Manager_C_Sharp.Models;

namespace Task_Manager_C_Sharp.Repository.Impl
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly TaskManagerCSharpContext _context;
        public UserRepositoryImpl(TaskManagerCSharpContext context)
        {
            _context = context;
        }

        public bool UserEmailExists(string email)
        {
            return _context.User.Any(user => user.Email.ToLower() == email.ToLower());
        }

        public User GetUserbyEmail(string email)
        {
            return _context.User.FirstOrDefault(user => user.Email.ToLower() == email.ToLower());
        }

        public void Save(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

        public User GetUserbyId(int userId)
        {
            return _context.User.FirstOrDefault(user => user.Id == userId);
        }
    }
}
