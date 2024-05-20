using System.ComponentModel.DataAnnotations;

namespace WebAppTest.DTO_s
{
    public class LoginDto
    {
        [Required]
        [MinLength(10)]
        [MaxLength(55)]
        public string UsernameOrEmail { get; set; }
        [Required]
        [MinLength(7)]
        [MaxLength(55)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe {  get; set; }
    }
}
