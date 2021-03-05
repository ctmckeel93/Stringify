using Microsoft.EntityFrameworkCore;

namespace Stringify.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        // public DbSet<Conve?rsation> Conversations { get; set; }

    }
}