using System.ComponentModel.DataAnnotations;

namespace ContosoShopEasy.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public bool IsEmailVerified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public List<Order> Orders { get; set; }
        public Address? ShippingAddress { get; set; }
        public Address? BillingAddress { get; set; }

        public User()
        {
            Username = string.Empty;
            Email = string.Empty;
            PasswordHash = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            PhoneNumber = string.Empty;
            Role = UserRole.Customer;
            IsActive = true;
            IsEmailVerified = false;
            CreatedDate = DateTime.UtcNow;
            Orders = new List<Order>();
        }

        public User(int id, string username, string email, string passwordHash, string firstName, string lastName)
        {
            Id = id;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = string.Empty;
            Role = UserRole.Customer;
            IsActive = true;
            IsEmailVerified = false;
            CreatedDate = DateTime.UtcNow;
            Orders = new List<Order>();
        }
    }

    public enum UserRole
    {
        Customer = 1,
        Admin = 2,
        Employee = 3
    }

    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public bool IsDefault { get; set; }
        public int UserId { get; set; }

        public Address()
        {
            Street = string.Empty;
            City = string.Empty;
            State = string.Empty;
            ZipCode = string.Empty;
            Country = string.Empty;
        }

        public Address(string street, string city, string state, string zipCode, string country)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
            Country = country;
        }

        public override string ToString()
        {
            return $"{Street}, {City}, {State} {ZipCode}, {Country}";
        }
    }
}