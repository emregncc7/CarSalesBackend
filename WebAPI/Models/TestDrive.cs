using System;

namespace WebAPI.Models
{
    public class TestDrive
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Status { get; set; } // Pending, Approved, Completed, Cancelled
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public Car Car { get; set; }
        public Customer Customer { get; set; }

        public TestDrive()
        {
            CreatedAt = DateTime.UtcNow;
            Status = "Pending";
        }
    }
} 