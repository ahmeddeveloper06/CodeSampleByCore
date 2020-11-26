using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTset.Models
{
    public partial class Country
    {
        public Country()
        {
            City = new HashSet<City>();
            Members = new HashSet<Members>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public bool IsDelete { get; set; }
        [Column("Created_By")]
        public int? CreatedBy { get; set; }
        [Column("Updated_By")]
        public int? UpdatedBy { get; set; }
        [Column("Created_At", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("Updated_At", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }

        [InverseProperty("Country")]
        public virtual ICollection<City> City { get; set; }
        [InverseProperty("Country")]
        public virtual ICollection<Members> Members { get; set; }
    }
}
