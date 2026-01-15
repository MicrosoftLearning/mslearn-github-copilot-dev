using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoDashboard.Models;

public class TaskItem
{
    [Key]
    public int TaskId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    [Required]
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    [Required]
    public TaskStatus Status { get; set; } = TaskStatus.NotStarted;

    public DateTime? DueDate { get; set; }

    [Required]
    public int AssignedUserId { get; set; }

    [Required]
    public int CreatedByUserId { get; set; }

    public int? ProjectId { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("AssignedUserId")]
    public virtual User AssignedUser { get; set; } = null!;

    [ForeignKey("CreatedByUserId")]
    public virtual User CreatedByUser { get; set; } = null!;

    [ForeignKey("ProjectId")]
    public virtual Project? Project { get; set; }

    public virtual ICollection<TaskComment> Comments { get; set; } = new List<TaskComment>();
}

public enum TaskPriority
{
    Low,
    Medium,
    High,
    Critical
}

public enum TaskStatus
{
    NotStarted,
    InProgress,
    Completed
}
