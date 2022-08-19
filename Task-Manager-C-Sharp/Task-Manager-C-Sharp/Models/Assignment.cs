using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Task_Manager_C_Sharp.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } 
        public DateTime EstimatedDate { get; set; }
        public DateTime? ConclusionDate { get; set; }

        [JsonIgnore]
        [ForeignKey("UserId")]
        public virtual User? user { get; private set; } 


    }
}
