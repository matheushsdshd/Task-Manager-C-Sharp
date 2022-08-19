using System.Text.Json.Serialization;

namespace Task_Manager_C_Sharp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public virtual ICollection<Assignment>? Assignments { get; private set; }
    }
}
