using Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Brand : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string LogoUrl { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
