using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTset.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class Members
    {
    }
  
    public class UserMetaData
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

       

    }
}
