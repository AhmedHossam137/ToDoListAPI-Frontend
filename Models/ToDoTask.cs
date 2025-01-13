using System;

namespace ToDoListAPI.Models
{
    public enum Priority
    {
        Low = 0,
        Medium = 1,
        High = 2
    }

    public class ToDoTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; } = "created";
        public Priority Priority { get; set; } = Priority.Medium; 
        public DateTime CreationDate { get; set; } = DateTime.UtcNow; // Auto-set creation date
        public DateTime? DueDate { get; set; }
        public string? Notes { get; set; }
    }
}