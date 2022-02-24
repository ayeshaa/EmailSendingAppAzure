using EmailSender.Model;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Context
{
    public class EmailSenderContext : DbContext
    {
        public DbSet<EmailSenderData> EmailSenderData { get; set; }
        public DbSet<EmailTemplates> EmailTemplates { get; set; }

        public EmailSenderContext(DbContextOptions<EmailSenderContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map entities to tables  
            modelBuilder.Entity<EmailSenderData>().ToTable("EmailSenderData");
            // Configure Primary Keys  
            modelBuilder.Entity<EmailSenderData>().HasKey(u => u.Id).HasName("PK_EmailSenderData");
            modelBuilder.Entity<EmailTemplates>().ToTable("EmailTemplates");
            // Configure Primary Keys  
            modelBuilder.Entity<EmailTemplates>().HasKey(u => u.Id).HasName("PK_EmailTemplates");
        }
    }
}
