namespace SaudisoftTask.Models
{
    using Areas.UserRole.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DbConnection : DbContext
    {
        public DbConnection()
            : base("name=DbConnection")
        {
        }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<LogedinUser> LogedinUsers { get; set; }

    }
}