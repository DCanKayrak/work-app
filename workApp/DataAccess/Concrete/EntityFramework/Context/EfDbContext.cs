using Core.Entity.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class EfDbContext : DbContext
    {
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<TaskCollection> TaskCollections { get; set; }
        public DbSet<Pomodoro> Pomodoros { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        private IConfiguration _configuration;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Server=localhost;Port=5433;Database=work_app;Integrated Security=true;Pooling=true;");
            
            base.OnConfiguring(optionsBuilder);
        }
    }
}
