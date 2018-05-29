using MessagesService.Models;
using Microsoft.EntityFrameworkCore;

namespace MessagesService.EntityFrameworkCore
{
    public class MessagesContext : DbContext
    {
        public MessagesContext(DbContextOptions<MessagesContext> options)
            : base(options)
        { }

        public DbSet<Message> Messages { get; set; }
    }
}