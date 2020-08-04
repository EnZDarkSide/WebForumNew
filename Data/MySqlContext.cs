using Microsoft.EntityFrameworkCore;
using WebForumNew.Classes;

namespace WebForumNew.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Section> Sections { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicMessage> TopicMessages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Password> Passwords { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;UserId=root;Password=;database=webforum;");
        }
    }
}