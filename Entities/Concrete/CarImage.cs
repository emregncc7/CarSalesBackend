using Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class CarImage : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        [StringLength(500)]
        public string ImagePath { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public bool IsMain { get; set; }

        [StringLength(100)]
        public string Caption { get; set; }

        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }
    }
}