﻿using System.ComponentModel.DataAnnotations;

namespace MessagesService.Models.Dto.MessageDto
{
    public class MessageInputModel
    {
        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string Author { get; set; }
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string Text { get; set; }
    }
}