using System;

namespace WebAPI.Models
{
    public class CarImage
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; }
        public string Caption { get; set; }
        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation property
        public Car Car { get; set; }

        public CarImage()
        {
            CreatedAt = DateTime.UtcNow;
            IsPrimary = false;
            SortOrder = 0;
        }
    }
} 