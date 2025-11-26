using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoDashboard.Models;

public class Project
{
    [Key]
    public int ProjectId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    [Required]
    public int ProjectManagerId { get; set; }

    public DateTime StartDate { get; set; } = DateTime.UtcNow;

    public DateTime? TargetCompletionDate { get; set; }

    [Required]
    public ProjectStatus Status { get; set; } = ProjectStatus.Planning;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("ProjectManagerId")]
    public virtual User ProjectManager { get; set; } = null!;

    public virtual ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

    // Computed property
    [NotMapped]
    public int CompletionPercentage
    {
        get
        {
            if (Tasks == null || !Tasks.Any()) return 0;
            var completedCount = Tasks.Count(t => t.Status == TaskStatus.Completed);
            return (int)((double)completedCount / Tasks.Count * 100);
        }
    }
}

public enum ProjectStatus
{
    Planning,
    Active,
    OnHold,
    Completed
}
