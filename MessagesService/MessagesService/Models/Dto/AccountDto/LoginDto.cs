using System.ComponentModel.DataAnnotations;

namespace MessagesService.Models.Dto.AccountDto
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}