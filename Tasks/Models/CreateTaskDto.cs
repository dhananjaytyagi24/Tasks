using System.ComponentModel.DataAnnotations;
using Tasks.Enums;

namespace Tasks.Models
{
    public class CreateTaskDto
    {
        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public ImportanceEnum Importance { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int DaysTaken { get; set; }
    }
}
