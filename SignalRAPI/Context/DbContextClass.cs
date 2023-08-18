using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SignalRAPI.Entities;
using SignalRAPI.Model;

namespace SignalRAPI.Context
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DB"));
        }

        public DbSet<NotiPolicy> NotiPolicy { get; set; }
        public DbSet<NotiPolicyLiveChatDetail> NotiPolicyLiveChatDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotiPolicy>().HasKey(o => o.id);
            modelBuilder.Entity<NotiPolicyLiveChatDetail>(entity => entity.HasNoKey());
        }
    }
}
