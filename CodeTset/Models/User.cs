using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTset.Models
{
    public partial class User
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string FullName { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(450)]
        public string AspNetUserId { get; set; }

        [ForeignKey(nameof(AspNetUserId))]
        [InverseProperty(nameof(AspNetUsers.User))]
        public virtual AspNetUsers AspNetUser { get; set; }
    }
}
