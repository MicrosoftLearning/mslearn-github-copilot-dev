using System.ComponentModel.DataAnnotations;

namespace ContosoDashboard.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string DisplayName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Department { get; set; }

    [MaxLength(100)]
    public string? JobTitle { get; set; }

    [Required]
    public UserRole Role { get; set; } = UserRole.Employee;

    [MaxLength(500)]
    public string? ProfilePhotoUrl { get; set; }

    public AvailabilityStatus AvailabilityStatus { get; set; } = AvailabilityStatus.Available;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? LastLoginDate { get; set; }

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    public bool EmailNotificationsEnabled { get; set; } = true;

    public bool InAppNotificationsEnabled { get; set; } = true;

    // Navigation properties
    public virtual ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();
    public virtual ICollection<TaskItem> CreatedTasks { get; set; } = new List<TaskItem>();
    public virtual ICollection<Project> ManagedProjects { get; set; } = new List<Project>();
    public virtual ICollection<ProjectMember> ProjectMemberships { get; set; } = new List<ProjectMember>();
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public virtual ICollection<TaskComment> Comments { get; set; } = new List<TaskComment>();
}

public enum UserRole
{
    Employee,
    TeamLead,
    ProjectManager,
    Administrator
}

public enum AvailabilityStatus
{
    Available,
    Busy,
    InMeeting,
    OutOfOffice
}
