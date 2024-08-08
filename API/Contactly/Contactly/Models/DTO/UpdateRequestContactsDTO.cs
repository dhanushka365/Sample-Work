using System.ComponentModel.DataAnnotations;

namespace Contactly.Models.DTO
{
    public class UpdateRequestContactsDTO
    {

        [Required]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
