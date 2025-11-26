using Microsoft.EntityFrameworkCore;
using ContosoDashboard.Data;
using ContosoDashboard.Models;

namespace ContosoDashboard.Services;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(int userId);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> CreateOrUpdateUserAsync(string email, string displayName);
    Task<bool> UpdateUserProfileAsync(User user);
    Task<bool> UpdateAvailabilityStatusAsync(int userId, AvailabilityStatus status);
    Task<List<User>> GetTeamMembersAsync(int userId);
}

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
    }

    public async Task<User> CreateOrUpdateUserAsync(string email, string displayName)
    {
        var user = await GetUserByEmailAsync(email);

        if (user == null)
        {
            user = new User
            {
                Email = email,
                DisplayName = displayName,
                Role = UserRole.Employee,
                AvailabilityStatus = AvailabilityStatus.Available,
                CreatedDate = DateTime.UtcNow
            };

            _context.Users.Add(user);
        }
        else
        {
            user.DisplayName = displayName;
            user.LastLoginDate = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> UpdateUserProfileAsync(User user)
    {
        var existingUser = await _context.Users.FindAsync(user.UserId);
        if (existingUser == null) return false;

        existingUser.DisplayName = user.DisplayName;
        existingUser.PhoneNumber = user.PhoneNumber;
        existingUser.Department = user.Department;
        existingUser.JobTitle = user.JobTitle;
        existingUser.ProfilePhotoUrl = user.ProfilePhotoUrl;
        existingUser.EmailNotificationsEnabled = user.EmailNotificationsEnabled;
        existingUser.InAppNotificationsEnabled = user.InAppNotificationsEnabled;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAvailabilityStatusAsync(int userId, AvailabilityStatus status)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        user.AvailabilityStatus = status;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<User>> GetTeamMembersAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return new List<User>();

        // Get users in the same department
        return await _context.Users
            .Where(u => u.Department == user.Department && u.UserId != userId)
            .OrderBy(u => u.DisplayName)
            .ToListAsync();
    }
}
