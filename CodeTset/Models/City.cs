using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTset.Models
{
    public partial class City
    {
        public City()
        {
            Members = new HashSet<Members>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("CountryID")]
        public int? CountryId { get; set; }
        public bool Active { get; set; }
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

        [ForeignKey(nameof(CountryId))]
        [InverseProperty("City")]
        public virtual Country Country { get; set; }
        [InverseProperty("City")]
        public virtual ICollection<Members> Members { get; set; }
    }
}
