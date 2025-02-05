using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class Car : IEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int BrandId { get; set; }
        
        [Required]
        public int ColorId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(4)]
        public string Year { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DailyPrice { get; set; }
        
        [Required]
        public int ModelYear { get; set; }
        
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Engine { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Transmission { get; set; }
        
        [Required]
        public int Mileage { get; set; }
        
        [Required]
        [StringLength(20)]
        public string FuelType { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        [StringLength(500)]
        public string ImageUrl { get; set; }
        
        [StringLength(20)]
        public string Telephone { get; set; }
        
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        
        [StringLength(50)]
        public string Instagram { get; set; }
        
        [Required]
        public bool IsAvailable { get; set; } = true;
        
        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }
        
        [ForeignKey("ColorId")]
        public virtual Color Color { get; set; }
        
        public virtual ICollection<CarImage> Images { get; set; }
    }
}
