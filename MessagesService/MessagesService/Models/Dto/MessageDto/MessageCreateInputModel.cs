using System.ComponentModel.DataAnnotations;

namespace MessagesService.Models.Dto.MessageDto
{
    public class MessageCreateInputModel
    {
        [Required]
        [StringLength(60,MinimumLength = 5)]
        public string Author { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string Text { get; set; }
    }
}