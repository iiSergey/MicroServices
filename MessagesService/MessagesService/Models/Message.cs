using System;

namespace MessagesService.Models
{
    public class Message
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}