using System;

namespace WebAPI.Models
{
    public class Inquiry
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Status { get; set; } // New, InProgress, Resolved, Closed
        public string Response { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? RespondedAt { get; set; }

        // Navigation properties
        public Car Car { get; set; }
        public Customer Customer { get; set; }

        public Inquiry()
        {
            CreatedAt = DateTime.UtcNow;
            Status = "New";
        }
    }
} 