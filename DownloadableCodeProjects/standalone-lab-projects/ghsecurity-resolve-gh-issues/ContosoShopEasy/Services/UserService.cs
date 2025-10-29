using System.Security.Cryptography;
using System.Text;
using ContosoShopEasy.Models;
using ContosoShopEasy.Data;

namespace ContosoShopEasy.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Vulnerable registration method - multiple security issues
        public bool RegisterUser(string username, string email, string password, string firstName, string lastName)
        {
            // Security vulnerability: Log sensitive information
            Console.WriteLine($"[DEBUG] Registering user: {username}, Email: {email}, Password: {password}");
            
            // Check if user already exists
            if (_userRepository.GetUserByUsername(username) != null)
            {
                Console.WriteLine($"User {username} already exists!");
                return false;
            }

            if (_userRepository.GetUserByEmail(email) != null)
            {
                Console.WriteLine($"Email {email} is already registered!");
                return false;
            }

            // Security vulnerability: Use weak MD5 hashing
            string passwordHash = GetMd5Hash(password);
            
            var user = new User
            {
                Id = _userRepository.GetNextUserId(),
                Username = username,
                Email = email,
                PasswordHash = passwordHash,
                FirstName = firstName,
                LastName = lastName,
                Role = UserRole.Customer,
                IsActive = true,
                IsEmailVerified = false,
                CreatedDate = DateTime.UtcNow
            };

            _userRepository.AddUser(user);
            
            // Security vulnerability: Log password hash
            Console.WriteLine($"[DEBUG] User created with password hash (MD5): {passwordHash}");
            
            return true;
        }

        // Vulnerable login method
        public User? LoginUser(string username, string password)
        {
            // Security vulnerability: Log password in plaintext
            Console.WriteLine($"[DEBUG] Login attempt for user: {username} with password: {password}");
            
            var user = _userRepository.GetUserByUsername(username);
            if (user == null)
            {
                Console.WriteLine($"User {username} not found!");
                return null;
            }

            string passwordHash = GetMd5Hash(password);
            if (user.PasswordHash == passwordHash)
            {
                user.LastLoginDate = DateTime.UtcNow;
                Console.WriteLine($"Login successful for user: {username}");
                return user;
            }

            Console.WriteLine($"Invalid password for user: {username}");
            return null;
        }

        public User? GetUser(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public User? GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }

        public User? GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public bool UpdateUser(User user)
        {
            return _userRepository.UpdateUser(user);
        }

        public bool DeleteUser(int userId)
        {
            return _userRepository.DeleteUser(userId);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        // Vulnerable method - uses weak MD5 hashing
        private string GetMd5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("X2"));
                return sb.ToString();
            }
        }

        // Method to validate user input - but with vulnerabilities
        public bool ValidateUserInput(string input, string fieldName)
        {
            // Security vulnerability: No proper input validation
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine($"[WARNING] Empty input for field: {fieldName}");
                return false;
            }

            // Security vulnerability: Accept potentially dangerous characters
            Console.WriteLine($"[DEBUG] Validating {fieldName}: {input}");
            return true;
        }
    }
}