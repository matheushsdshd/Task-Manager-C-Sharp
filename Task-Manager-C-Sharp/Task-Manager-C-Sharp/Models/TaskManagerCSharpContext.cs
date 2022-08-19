using Microsoft.EntityFrameworkCore;

namespace Task_Manager_C_Sharp.Models
{
    public class TaskManagerCSharpContext : DbContext
    {
        public TaskManagerCSharpContext(DbContextOptions<TaskManagerCSharpContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Assignment> Assignment { get; set; }
    }
}
