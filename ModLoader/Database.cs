using Microsoft.EntityFrameworkCore;
using ModLoader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ModLoader
{
    public class Database : DbContext
    {
        //TODO: EF
        public async Task<IEnumerable<Mod>> GetMods() => await Mods.AsNoTracking().ToListAsync();

        public async Task SaveAsync(Mod mod)
        {
            Mods.Attach(mod);
            //Thread.Sleep(2500); // test async
            await SaveChangesAsync();
        }

        public DbSet<Mod> Mods { get; set; }
        public DbSet<WebResource> WebResources { get; set; }
        public DbSet<Pack> Packs { get; set; }
        //public DbSet<ModPack> ModPack { get; set; }


        public string DbPath { get; }

        public Database()
        {
            DbPath = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "ModLoader.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Mod>(entity => { entity.Property(x => x.IsChecked).IsRequired(); });

            #region ModsSeed
            modelBuilder.Entity<Mod>()
                .HasData(
                    new Mod { Id = 1, Name="Mod_1", Description = "Walk the dog" , Icon="C:\\test.png", isDelited=false},
                    new Mod { Id = 2, Name = "Mod_2", Description = "Buy some milk", Icon = "C:\\test.png", isDelited = false },
                    new Mod { Id = 3, Name = "Mod_3", Description = "Learn Avalonia", Icon = "C:\\test.png", isDelited = false });
            #endregion

        }

    }
}
