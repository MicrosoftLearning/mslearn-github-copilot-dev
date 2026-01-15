using Microsoft.EntityFrameworkCore;
using ContosoDashboard.Data;
using ContosoDashboard.Models;

namespace ContosoDashboard.Services;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(int userId);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> CreateOrUpdateUserAsync(string email, string displayName);
    Task<bool> UpdateUserProfileAsync(User user, int requestingUserId);
    Task<bool> UpdateAvailabilityStatusAsync(int userId, AvailabilityStatus status);
    Task<List<User>> GetTeamMembersAsync(int userId);
    Task<List<User>> GetAllUsersAsync();
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

    public async Task<bool> UpdateUserProfileAsync(User user, int requestingUserId)
    {
        var existingUser = await _context.Users.FindAsync(user.UserId);
        if (existingUser == null) return false;

        // Authorization: Users can only update their own profile
        if (existingUser.UserId != requestingUserId)
        {
            return false; // User not authorized to update this profile
        }

        // Input validation
        if (!string.IsNullOrWhiteSpace(user.DisplayName))
            existingUser.DisplayName = user.DisplayName;
        
        // Validate phone number format if provided
        if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
        {
            if (user.PhoneNumber.Length <= 20)
                existingUser.PhoneNumber = user.PhoneNumber;
        }
        else
        {
            existingUser.PhoneNumber = null;
        }

        // Validate and sanitize department and job title
        if (!string.IsNullOrWhiteSpace(user.Department) && user.Department.Length <= 100)
            existingUser.Department = user.Department;
        
        if (!string.IsNullOrWhiteSpace(user.JobTitle) && user.JobTitle.Length <= 100)
            existingUser.JobTitle = user.JobTitle;
        
        // Validate profile photo URL
        if (!string.IsNullOrWhiteSpace(user.ProfilePhotoUrl))
        {
            if (Uri.TryCreate(user.ProfilePhotoUrl, UriKind.Absolute, out var uri) && 
                (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps) &&
                user.ProfilePhotoUrl.Length <= 500)
            {
                existingUser.ProfilePhotoUrl = user.ProfilePhotoUrl;
            }
        }
        else
        {
            existingUser.ProfilePhotoUrl = null;
        }

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

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users
            .OrderBy(u => u.DisplayName)
            .ToListAsync();
    }
}
