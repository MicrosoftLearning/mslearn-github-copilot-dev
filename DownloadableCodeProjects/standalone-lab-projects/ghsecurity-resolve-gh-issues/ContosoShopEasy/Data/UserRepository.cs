using ContosoShopEasy.Models;

namespace ContosoShopEasy.Data
{
    public class UserRepository
    {
        private static List<User> _users = new List<User>();
        private static int _nextUserId = 1;

        static UserRepository()
        {
            InitializeUsers();
        }

        private static void InitializeUsers()
        {
            // Pre-populate with some test users (with weak password hashes for demo)
            _users.AddRange(new List<User>
            {
                new User(1, "john_doe", "john.doe@email.com", "5D41402ABC4B2A76B9719D911017C592", "John", "Doe") // MD5 hash of "hello"
                {
                    PhoneNumber = "555-0123",
                    DateOfBirth = new DateTime(1990, 5, 15),
                    Role = UserRole.Customer,
                    IsEmailVerified = true,
                    LastLoginDate = DateTime.UtcNow.AddDays(-2),
                    ShippingAddress = new Address("123 Main St", "Anytown", "CA", "12345", "USA"),
                    BillingAddress = new Address("123 Main St", "Anytown", "CA", "12345", "USA")
                },
                new User(2, "jane_smith", "jane.smith@email.com", "098F6BCD4621D373CADE4E832627B4F6", "Jane", "Smith") // MD5 hash of "test"
                {
                    PhoneNumber = "555-0456",
                    DateOfBirth = new DateTime(1985, 8, 22),
                    Role = UserRole.Customer,
                    IsEmailVerified = true,
                    LastLoginDate = DateTime.UtcNow.AddHours(-6),
                    ShippingAddress = new Address("456 Oak Ave", "Springfield", "TX", "67890", "USA"),
                    BillingAddress = new Address("456 Oak Ave", "Springfield", "TX", "67890", "USA")
                },
                new User(3, "admin", "admin@contoso.com", "5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8", "Admin", "User") // MD5 hash of "password"
                {
                    PhoneNumber = "555-0001",
                    DateOfBirth = new DateTime(1980, 1, 1),
                    Role = UserRole.Admin,
                    IsEmailVerified = true,
                    LastLoginDate = DateTime.UtcNow.AddMinutes(-30),
                    ShippingAddress = new Address("789 Corporate Blvd", "Business City", "NY", "10001", "USA"),
                    BillingAddress = new Address("789 Corporate Blvd", "Business City", "NY", "10001", "USA")
                },
                new User(4, "mike_wilson", "mike.wilson@email.com", "25D55AD283AA400AF464C76D713C07AD", "Mike", "Wilson") // MD5 hash of "123456"
                {
                    PhoneNumber = "555-0789",
                    DateOfBirth = new DateTime(1992, 12, 3),
                    Role = UserRole.Customer,
                    IsEmailVerified = false,
                    LastLoginDate = DateTime.UtcNow.AddDays(-10),
                    ShippingAddress = new Address("321 Pine St", "Riverside", "FL", "33101", "USA"),
                    BillingAddress = new Address("321 Pine St", "Riverside", "FL", "33101", "USA")
                },
                new User(5, "sarah_johnson", "sarah.johnson@email.com", "E10ADC3949BA59ABBE56E057F20F883E", "Sarah", "Johnson") // MD5 hash of "123456"
                {
                    PhoneNumber = "555-0321",
                    DateOfBirth = new DateTime(1988, 7, 18),
                    Role = UserRole.Employee,
                    IsEmailVerified = true,
                    LastLoginDate = DateTime.UtcNow.AddDays(-1),
                    ShippingAddress = new Address("654 Elm Dr", "Hometown", "WA", "98001", "USA"),
                    BillingAddress = new Address("654 Elm Dr", "Hometown", "WA", "98001", "USA")
                }
            });

            _nextUserId = _users.Count + 1;
        }

        public List<User> GetAllUsers()
        {
            return _users.ToList();
        }

        public User? GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id && u.IsActive);
        }

        public User? GetUserByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            return _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.IsActive);
        }

        public User? GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            return _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.IsActive);
        }

        public void AddUser(User user)
        {
            user.Id = _nextUserId++;
            user.CreatedDate = DateTime.UtcNow;
            _users.Add(user);
        }

        public bool UpdateUser(User user)
        {
            var existingUser = GetUserById(user.Id);
            if (existingUser != null)
            {
                var index = _users.IndexOf(existingUser);
                _users[index] = user;
                return true;
            }
            return false;
        }

        public bool DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                user.IsActive = false;
                return true;
            }
            return false;
        }

        public List<User> GetUsersByRole(UserRole role)
        {
            return _users.Where(u => u.Role == role && u.IsActive).ToList();
        }

        public List<User> SearchUsers(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return new List<User>();

            searchTerm = searchTerm.ToLower();
            return _users.Where(u => u.IsActive &&
                (u.Username.ToLower().Contains(searchTerm) ||
                 u.Email.ToLower().Contains(searchTerm) ||
                 u.FirstName.ToLower().Contains(searchTerm) ||
                 u.LastName.ToLower().Contains(searchTerm)))
                .ToList();
        }

        public int GetNextUserId()
        {
            return _nextUserId;
        }

        public bool IsUsernameAvailable(string username)
        {
            return !_users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.IsActive);
        }

        public bool IsEmailAvailable(string email)
        {
            return !_users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.IsActive);
        }

        // Security vulnerability: Method to get user passwords (should never exist)
        public string GetUserPassword(string username)
        {
            var user = GetUserByUsername(username);
            return user?.PasswordHash ?? string.Empty;
        }

        // Security vulnerability: Method to get all user credentials
        public Dictionary<string, string> GetAllUserCredentials()
        {
            var credentials = new Dictionary<string, string>();
            foreach (var user in _users.Where(u => u.IsActive))
            {
                credentials[user.Username] = user.PasswordHash;
            }
            return credentials;
        }
    }
}