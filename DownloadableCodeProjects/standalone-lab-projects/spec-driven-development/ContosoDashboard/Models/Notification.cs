using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoDashboard.Models;

public class Notification
{
    [Key]
    public int NotificationId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Message { get; set; } = string.Empty;

    [Required]
    public NotificationType Type { get; set; }

    [Required]
    public NotificationPriority Priority { get; set; } = NotificationPriority.Informational;

    public bool IsRead { get; set; } = false;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}

public enum NotificationType
{
    TaskAssignment,
    TaskUpdate,
    TaskDueSoon,
    TaskCompleted,
    TaskComment,
    ProjectUpdate,
    SystemAnnouncement
}

public enum NotificationPriority
{
    Urgent,
    Important,
    Informational
}
