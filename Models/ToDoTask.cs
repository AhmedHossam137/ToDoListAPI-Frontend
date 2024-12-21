namespace ToDoListAPI.Models
{
    public class ToDoTask
    {
        public int Id { get; set; } 
        public string Title { get; set; } 
        public string? Description { get; set; } 
        public string Status { get; set; } = "created"; 
        public string Priority { get; set; } = "medium"; 
        public DateTime CreationDate { get; set; } = DateTime.UtcNow; // Auto-set creation date
        public DateTime? DueDate { get; set; } 
        public string? Notes { get; set; } 
    }
}
