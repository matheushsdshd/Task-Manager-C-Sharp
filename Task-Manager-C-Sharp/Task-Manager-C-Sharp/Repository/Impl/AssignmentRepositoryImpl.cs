using Task_Manager_C_Sharp.Models;

namespace Task_Manager_C_Sharp.Repository.Impl
{
    public class AssignmentRepositoryImpl : IAssignmentRepository
    {
        private readonly TaskManagerCSharpContext _context;

        public AssignmentRepositoryImpl(TaskManagerCSharpContext context)
        {
            _context = context;
        }

        public void CreateAssignment(Assignment assignment)
        {
            _context.Assignment.Add(assignment);
            _context.SaveChanges();
        }
    }
}
