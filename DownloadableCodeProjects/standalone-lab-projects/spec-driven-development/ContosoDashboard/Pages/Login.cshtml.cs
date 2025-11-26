using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using ContosoDashboard.Services;
using ContosoDashboard.Models;

namespace ContosoDashboard.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        public List<User>? Users { get; set; }
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            // Load all users for the dropdown
            Users = await _userService.GetAllUsersAsync();
        }

        public async Task<IActionResult> OnPostAsync(int selectedUserId)
        {
            Console.WriteLine($"Login POST: selectedUserId = {selectedUserId}");
            
            // Reload users for the form in case of error
            Users = await _userService.GetAllUsersAsync();

            if (selectedUserId == 0)
            {
                Console.WriteLine("Login POST: No user selected");
                ErrorMessage = "Please select a user";
                return Page();
            }

            var user = await _userService.GetUserByIdAsync(selectedUserId);

            if (user == null)
            {
                Console.WriteLine($"Login POST: User {selectedUserId} not found");
                ErrorMessage = "User not found";
                return Page();
            }

            try
            {
                Console.WriteLine($"Login POST: Attempting to sign in user {user.DisplayName}");
                
                // Create claims for the authenticated user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.DisplayName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                Console.WriteLine($"Login POST: Sign in successful, redirecting to /");
                
                // Update last login date
                user.LastLoginDate = DateTime.UtcNow;
                await _userService.UpdateUserProfileAsync(user, user.UserId);

                // Redirect to home page
                return Redirect("/");
            }
            catch (Exception ex)
            {
                // Log the actual error but show generic message to user
                Console.WriteLine($"Login error: {ex.Message}");
                Console.WriteLine($"Login error stack: {ex.StackTrace}");
                ErrorMessage = "Login failed. Please try again.";
                return Page();
            }
        }
    }
}
