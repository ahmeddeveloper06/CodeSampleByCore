using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTset.Models
{
    public partial class Members
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("First_Name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Column("Last_Name")]
        [StringLength(50)]
        public string LastName { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        [StringLength(50)]
        public string Gender { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BirthDate { get; set; }
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(250)]
        public string Notes { get; set; }
        public bool Active { get; set; }
        [Column("image")]
        [StringLength(50)]
        public string Image { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty("Members")]
        public virtual City City { get; set; }
        [ForeignKey(nameof(CountryId))]
        [InverseProperty("Members")]
        public virtual Country Country { get; set; }
    }
}
