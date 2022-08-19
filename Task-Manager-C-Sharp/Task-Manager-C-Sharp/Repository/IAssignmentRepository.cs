using Task_Manager_C_Sharp.Models;

namespace Task_Manager_C_Sharp.Repository
{
    public interface IAssignmentRepository
    {
        public void CreateAssignment(Assignment assignment);
    }
}
