using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamScapeInteractive.Model;

namespace DreamScapeInteractive.Data
{
    public class AppDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Item> Items { get; set; } 
        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<Trade> Trades { get; set; }
        public DbSet<TradeItem> TradeItems { get; set; }
        public DbSet<Notification> Notifications { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;port=3306;user=root;password=;database=DreamScapeInteractive;",
                ServerVersion.AutoDetect("server=localhost;port=3306;user=root;password=;database=DreamScapeInteractive;")
            );

        }
    }
}
