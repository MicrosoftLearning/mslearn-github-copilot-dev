using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoDashboard.Models;

public class ProjectMember
{
    [Key]
    public int ProjectMemberId { get; set; }

    [Required]
    public int ProjectId { get; set; }

    [Required]
    public int UserId { get; set; }

    [MaxLength(50)]
    public string Role { get; set; } = "TeamMember";

    public DateTime AssignedDate { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("ProjectId")]
    public virtual Project Project { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}
