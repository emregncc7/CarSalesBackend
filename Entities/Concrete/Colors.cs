using Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Color : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(7)]
        public string HexCode { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
