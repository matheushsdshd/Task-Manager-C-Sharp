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
        public void Save(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }
    }
}
