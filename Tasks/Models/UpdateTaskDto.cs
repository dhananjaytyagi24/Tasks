﻿using System.ComponentModel.DataAnnotations;
using Tasks.Enums;

namespace Tasks.Models
{
    public class UpdateTaskDto
    {
        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public ImportanceEnum Importance { get; set; }

        [Required]
        public int DaysTaken { get; set; }

        public bool IsCompleted { get; set; }
    }
}
