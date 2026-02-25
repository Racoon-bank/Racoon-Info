using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public class EditProfileDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(1000, ErrorMessage = " The field Username must be a string or array type with a maximum length of '1000'.")]
        public string Username { get; set; } = string.Empty;
    }
}