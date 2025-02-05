using System;
using Core.Entities;

namespace Core.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }

        public User()
        {
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
            Role = "User"; // Default role
            Status = "Active"; // Default status
        }
    }
} 