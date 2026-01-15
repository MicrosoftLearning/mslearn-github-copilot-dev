using Microsoft.EntityFrameworkCore;
using ContosoDashboard.Data;
using ContosoDashboard.Models;

namespace ContosoDashboard.Services;

public interface ITaskService
{
    Task<List<TaskItem>> GetUserTasksAsync(int userId);
    Task<List<TaskItem>> GetFilteredTasksAsync(int userId, Models.TaskStatus? status, TaskPriority? priority, int? projectId);
    Task<TaskItem?> GetTaskByIdAsync(int taskId, int requestingUserId);
    Task<TaskItem> CreateTaskAsync(TaskItem task);
    Task<bool> UpdateTaskStatusAsync(int taskId, int requestingUserId, Models.TaskStatus status);
    Task<bool> AddTaskCommentAsync(int taskId, int userId, string comment);
    Task<List<TaskComment>> GetTaskCommentsAsync(int taskId, int requestingUserId);
}

public class TaskService : ITaskService
{
    private readonly ApplicationDbContext _context;
    private readonly INotificationService _notificationService;

    public TaskService(ApplicationDbContext context, INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    public async Task<List<TaskItem>> GetUserTasksAsync(int userId)
    {
        return await _context.Tasks
            .Include(t => t.AssignedUser)
            .Include(t => t.CreatedByUser)
            .Include(t => t.Project)
            .Where(t => t.AssignedUserId == userId)
            .OrderByDescending(t => t.Priority)
            .ThenBy(t => t.DueDate)
            .ToListAsync();
    }

    public async Task<List<TaskItem>> GetFilteredTasksAsync(int userId, Models.TaskStatus? status, TaskPriority? priority, int? projectId)
    {
        var query = _context.Tasks
            .Include(t => t.AssignedUser)
            .Include(t => t.CreatedByUser)
            .Include(t => t.Project)
            .Where(t => t.AssignedUserId == userId);

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        if (priority.HasValue)
            query = query.Where(t => t.Priority == priority.Value);

        if (projectId.HasValue)
            query = query.Where(t => t.ProjectId == projectId.Value);

        return await query
            .OrderByDescending(t => t.Priority)
            .ThenBy(t => t.DueDate)
            .ToListAsync();
    }

    public async Task<TaskItem?> GetTaskByIdAsync(int taskId, int requestingUserId)
    {
        var task = await _context.Tasks
            .Include(t => t.AssignedUser)
            .Include(t => t.CreatedByUser)
            .Include(t => t.Project)
            .ThenInclude(p => p.ProjectMembers)
            .Include(t => t.Comments)
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(t => t.TaskId == taskId);

        if (task == null) return null;

        // Authorization: User can only view tasks they are assigned to, created, or are part of the project
        var isAssignedUser = task.AssignedUserId == requestingUserId;
        var isCreator = task.CreatedByUserId == requestingUserId;
        var isProjectMember = task.Project?.ProjectMembers.Any(pm => pm.UserId == requestingUserId) ?? false;
        var isProjectManager = task.Project?.ProjectManagerId == requestingUserId;

        if (!isAssignedUser && !isCreator && !isProjectMember && !isProjectManager)
        {
            return null; // User not authorized to view this task
        }

        return task;
    }

    public async Task<TaskItem> CreateTaskAsync(TaskItem task)
    {
        task.CreatedDate = DateTime.UtcNow;
        task.UpdatedDate = DateTime.UtcNow;

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        // Create notification for assigned user
        await _notificationService.CreateNotificationAsync(new Notification
        {
            UserId = task.AssignedUserId,
            Title = "New Task Assigned",
            Message = $"You have been assigned a new task: {task.Title}",
            Type = NotificationType.TaskAssignment,
            Priority = task.Priority == TaskPriority.Critical ? NotificationPriority.Urgent : NotificationPriority.Important
        });

        return task;
    }

    public async Task<bool> UpdateTaskStatusAsync(int taskId, int requestingUserId, Models.TaskStatus status)
    {
        var task = await _context.Tasks
            .Include(t => t.Project)
            .ThenInclude(p => p.ProjectMembers)
            .FirstOrDefaultAsync(t => t.TaskId == taskId);
            
        if (task == null) return false;

        // Authorization: Only assigned user, creator, project manager, or project members can update status
        var isAssignedUser = task.AssignedUserId == requestingUserId;
        var isCreator = task.CreatedByUserId == requestingUserId;
        var isProjectMember = task.Project?.ProjectMembers.Any(pm => pm.UserId == requestingUserId) ?? false;
        var isProjectManager = task.Project?.ProjectManagerId == requestingUserId;

        if (!isAssignedUser && !isCreator && !isProjectMember && !isProjectManager)
        {
            return false; // User not authorized to update this task
        }

        var oldStatus = task.Status;
        task.Status = status;
        task.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        // Send notification if task is completed
        if (status == Models.TaskStatus.Completed)
        {
            await _notificationService.CreateNotificationAsync(new Notification
            {
                UserId = task.CreatedByUserId,
                Title = "Task Completed",
                Message = $"Task '{task.Title}' has been completed",
                Type = NotificationType.TaskCompleted,
                Priority = NotificationPriority.Informational
            });
        }

        return true;
    }

    public async Task<bool> AddTaskCommentAsync(int taskId, int userId, string comment)
    {
        var task = await _context.Tasks.FindAsync(taskId);
        if (task == null) return false;

        var taskComment = new TaskComment
        {
            TaskId = taskId,
            UserId = userId,
            CommentText = comment,
            CreatedDate = DateTime.UtcNow
        };

        _context.TaskComments.Add(taskComment);
        await _context.SaveChangesAsync();

        // Notify task assignee if commenter is different
        if (userId != task.AssignedUserId)
        {
            await _notificationService.CreateNotificationAsync(new Notification
            {
                UserId = task.AssignedUserId,
                Title = "New Comment on Task",
                Message = $"A comment was added to task: {task.Title}",
                Type = NotificationType.TaskComment,
                Priority = NotificationPriority.Informational
            });
        }

        return true;
    }

    public async Task<List<TaskComment>> GetTaskCommentsAsync(int taskId, int requestingUserId)
    {
        var task = await _context.Tasks
            .Include(t => t.Project)
            .ThenInclude(p => p.ProjectMembers)
            .FirstOrDefaultAsync(t => t.TaskId == taskId);

        if (task == null) return new List<TaskComment>();

        // Authorization: User can only view comments if they have access to the task
        var isAssignedUser = task.AssignedUserId == requestingUserId;
        var isCreator = task.CreatedByUserId == requestingUserId;
        var isProjectMember = task.Project?.ProjectMembers.Any(pm => pm.UserId == requestingUserId) ?? false;
        var isProjectManager = task.Project?.ProjectManagerId == requestingUserId;

        if (!isAssignedUser && !isCreator && !isProjectMember && !isProjectManager)
        {
            return new List<TaskComment>(); // User not authorized
        }

        return await _context.TaskComments
            .Include(c => c.User)
            .Where(c => c.TaskId == taskId)
            .OrderBy(c => c.CreatedDate)
            .ToListAsync();
    }
}
