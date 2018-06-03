using System;

namespace MessagesService.Models.Dto.MessageDto
{
    public class MessageOutputModel
    {
        public string Author { get; set; }
        public DateTime CreatedOn { get; set; }
        public long Id { get; set; }
        public string Text { get; set; }
    }
}