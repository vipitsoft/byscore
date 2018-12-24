using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYSCORE.Entity
{
    [Table("product")]
    public class Product : BaseEntity<long>
    {

        [Column("name")]
        [StringLength(20)]
        [Required]
        public string Name { get; set; }

        [Column("description")]
        [StringLength(500)]
        [Required]
        public string Description { get; set; }

        [Column("category")]
        [Range(1, int.MaxValue)]
        public int Category { get; set; } = 1;

        [Column("price")]
        [Required]
        public decimal Price { get; set; }

        [Column("discout")]
        public decimal Discout { get; set; }
    }

}
