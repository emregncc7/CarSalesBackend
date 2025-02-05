using Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class Inquiry : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(200)]
        public string Subject { get; set; }

        [Required]
        [StringLength(2000)]
        public string Message { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // New, InProgress, Responded, Closed

        [StringLength(2000)]
        public string Response { get; set; }

        public DateTime? ResponseDate { get; set; }

        [Required]
        public bool IsUrgent { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
} 