using Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class TestDrive : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime ScheduledDate { get; set; }

        [Required]
        public TimeSpan ScheduledTime { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // Pending, Confirmed, Completed, Cancelled

        [StringLength(500)]
        public string Notes { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
} 