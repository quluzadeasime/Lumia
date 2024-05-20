using System.ComponentModel.DataAnnotations;

namespace WebAppTest.DTO_s
{
    public class RegisterDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Firstname { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Lastname { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password),Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
