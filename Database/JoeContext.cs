using Joebot_Backend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Joebot_Backend.Database
{
    public class JoeContext : DbContext
    {
        public JoeContext(DbContextOptions<JoeContext> options) : base(options) { }

        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<StatusMessage> StatusMessages { get; set; }
        public virtual DbSet<Trigger> Triggers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TriggerWord> TriggerWords { get; set; }
        public virtual DbSet<TriggerResponse> TriggerResponses { get; set; }
        public virtual DbSet<ReactEmote> ReactEmote { get; set; }
    }
}