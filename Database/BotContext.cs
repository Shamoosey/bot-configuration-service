using DiscordBot_Backend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot_Backend.Database
{
    public class BotContext : DbContext
    {
        public BotContext(DbContextOptions<BotContext> options) : base(options) { }

        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<StatusMessage> StatusMessages { get; set; }
        public virtual DbSet<Trigger> Triggers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TriggerWord> TriggerWords { get; set; }
        public virtual DbSet<TriggerResponse> TriggerResponses { get; set; }
        public virtual DbSet<ReactEmote> ReactEmote { get; set; }
    }
}