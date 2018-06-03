using MessagesService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MessagesService.EntityFrameworkCore
{
    public class MessagesContext : IdentityDbContext
    {
        public MessagesContext(DbContextOptions<MessagesContext> options)
            : base(options)
        { }

        public DbSet<Message> Messages { get; set; }
    }
}