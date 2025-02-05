using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
        public string BodyType { get; set; }
        public string EngineSize { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<CarImage> Images { get; set; }
        public ICollection<TestDrive> TestDrives { get; set; }
        public ICollection<Inquiry> Inquiries { get; set; }

        public Car()
        {
            Images = new List<CarImage>();
            TestDrives = new List<TestDrive>();
            Inquiries = new List<Inquiry>();
            CreatedAt = DateTime.UtcNow;
            IsAvailable = true;
        }
    }
} 