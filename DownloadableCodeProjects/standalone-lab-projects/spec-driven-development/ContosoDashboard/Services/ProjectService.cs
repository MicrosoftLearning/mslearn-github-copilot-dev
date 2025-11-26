using Microsoft.EntityFrameworkCore;
using ContosoDashboard.Data;
using ContosoDashboard.Models;

namespace ContosoDashboard.Services;

public interface IProjectService
{
    Task<List<Project>> GetUserProjectsAsync(int userId);
    Task<Project?> GetProjectByIdAsync(int projectId);
    Task<Project> CreateProjectAsync(Project project);
    Task<bool> UpdateProjectAsync(Project project);
    Task<bool> AddProjectMemberAsync(int projectId, int userId, string role);
    Task<List<ProjectMember>> GetProjectMembersAsync(int projectId);
}

public class ProjectService : IProjectService
{
    private readonly ApplicationDbContext _context;

    public ProjectService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Project>> GetUserProjectsAsync(int userId)
    {
        // Get projects where user is manager or a member
        var managedProjects = _context.Projects
            .Where(p => p.ProjectManagerId == userId);

        var memberProjects = _context.Projects
            .Where(p => p.ProjectMembers.Any(pm => pm.UserId == userId));

        var projects = await managedProjects
            .Union(memberProjects)
            .Include(p => p.ProjectManager)
            .Include(p => p.Tasks)
            .Include(p => p.ProjectMembers)
            .ThenInclude(pm => pm.User)
            .OrderByDescending(p => p.CreatedDate)
            .ToListAsync();

        return projects;
    }

    public async Task<Project?> GetProjectByIdAsync(int projectId)
    {
        return await _context.Projects
            .Include(p => p.ProjectManager)
            .Include(p => p.Tasks)
            .ThenInclude(t => t.AssignedUser)
            .Include(p => p.ProjectMembers)
            .ThenInclude(pm => pm.User)
            .FirstOrDefaultAsync(p => p.ProjectId == projectId);
    }

    public async Task<Project> CreateProjectAsync(Project project)
    {
        project.CreatedDate = DateTime.UtcNow;
        project.UpdatedDate = DateTime.UtcNow;

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return project;
    }

    public async Task<bool> UpdateProjectAsync(Project project)
    {
        var existingProject = await _context.Projects.FindAsync(project.ProjectId);
        if (existingProject == null) return false;

        existingProject.Name = project.Name;
        existingProject.Description = project.Description;
        existingProject.Status = project.Status;
        existingProject.TargetCompletionDate = project.TargetCompletionDate;
        existingProject.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddProjectMemberAsync(int projectId, int userId, string role)
    {
        var project = await _context.Projects.FindAsync(projectId);
        if (project == null) return false;

        var existingMember = await _context.ProjectMembers
            .FirstOrDefaultAsync(pm => pm.ProjectId == projectId && pm.UserId == userId);

        if (existingMember != null) return false;

        var projectMember = new ProjectMember
        {
            ProjectId = projectId,
            UserId = userId,
            Role = role,
            AssignedDate = DateTime.UtcNow
        };

        _context.ProjectMembers.Add(projectMember);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<ProjectMember>> GetProjectMembersAsync(int projectId)
    {
        return await _context.ProjectMembers
            .Include(pm => pm.User)
            .Where(pm => pm.ProjectId == projectId)
            .ToListAsync();
    }
}
