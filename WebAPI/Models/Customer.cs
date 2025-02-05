using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<TestDrive> TestDrives { get; set; }
        public ICollection<Inquiry> Inquiries { get; set; }

        public Customer()
        {
            TestDrives = new List<TestDrive>();
            Inquiries = new List<Inquiry>();
            CreatedAt = DateTime.UtcNow;
        }
    }
} 