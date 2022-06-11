using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tickets.Data.Base;

namespace tickets.Models
{
    public class Producer:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is required")]
        public string ProfilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Biography is required")]
        public string Biography { get; set; }

        // Relations 

        public List<Movie> Movies { get; set; }
    }
}
