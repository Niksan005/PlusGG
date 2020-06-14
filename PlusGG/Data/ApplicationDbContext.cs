using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlusGG.Data.Models;

namespace PlusGG.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Champion> Champions { get; set; }
        public DbSet<ChampionRunes> ChampionRunes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemSet> ItemSets { get; set; }
        public DbSet<MainRunes> MainRunes { get; set; }
        public DbSet<MatchUp> MatchUps { get; set; }
        public DbSet<RuneCategories> RuneCategories { get; set; }
        public DbSet<Runes> Runes { get; set; }
        public DbSet<Spell> Spells { get; set; }
        public DbSet<SummonerSpell> SummonerSpells { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Champion>()
                .HasMany(x => x.MatchUpsMain)
                .WithOne(x => x.MainChampion);

            builder.Entity<Champion>()
                .HasMany(x => x.MatchUpsVs)
                .WithOne(x => x.VsChampion);

            builder.Entity<Champion>()
                .HasMany(x => x.Spells)
                .WithOne(x => x.Champion);

            builder.Entity<Champion>()
                .HasOne(x => x.SummonerSpellD)
                .WithMany(x => x.ChampionD);

            builder.Entity<Champion>()
                .HasOne(x => x.SummonerSpellF)
                .WithMany(x => x.ChampionF);

            builder.Entity<ChampionRunes>()
                .HasOne(x => x.PrimaryMainRune)
                .WithMany(x => x.ChampionRunes);

            builder.Entity<ChampionRunes>()
                .HasOne(x => x.PrimaryRune1)
                .WithMany(x => x.PrimaryRune1);

            builder.Entity<ChampionRunes>()
                .HasOne(x => x.PrimaryRune2)
                .WithMany(x => x.PrimaryRune2);

            builder.Entity<ChampionRunes>()
                .HasOne(x => x.PrimaryRune3)
                .WithMany(x => x.PrimaryRune3);

            builder.Entity<ChampionRunes>()
                .HasOne(x => x.SecondaryRune1)
                .WithMany(x => x.SecondaryRune1);

            builder.Entity<ChampionRunes>()
                .HasOne(x => x.SecondaryRune2)
                .WithMany(x => x.SecondaryRune2);

            builder.Entity<ChampionRunes>()
                .HasOne(x => x.SecondaryRune3)
                .WithMany(x => x.SecondaryRune3);

            builder.Entity<ChampionRunes>()
                .HasOne(x => x.MatchUp)
                .WithOne(x => x.MainChampionRunes)
                .HasForeignKey<MatchUp>(x => x.Id);

            builder.Entity<Item>()
                .HasMany(x => x.ItemSets)
                .WithOne(x => x.Item);

            builder.Entity<MatchUp>()
                .HasMany(x => x.ItemSets)
                .WithOne(x => x.MatchUp);

            builder.Entity<RuneCategories>()
                .HasMany(x => x.MainRunes)
                .WithOne(x => x.RuneCategory);

            builder.Entity<RuneCategories>()
                .HasMany(x => x.Runes)
                .WithOne(x => x.RuneCategory);

            builder.Entity<RuneCategories>()
                .HasMany(x => x.Runes)
                .WithOne(x => x.RuneCategory);




            /// <summary>
            /// Setup Admin Seed
            /// </summary>
            const string AdminRoleId = "1b821ed5-85f0-4dab-85d8-626596989a2b";
            const string AdminId = "444955db-cdf3-4934-91b0-fc6ce4f53154";

            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = AdminRoleId, Name = "Admin", NormalizedName = "Admin".ToUpper() });

            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = AdminId,
                    UserName = "Admin@admin",
                    NormalizedUserName = "admin@admin",
                    Email = "Admin@admin",
                    NormalizedEmail = "admin@admin",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "AtAdmin"), // dont look at that

                }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = AdminRoleId,
                UserId = AdminId
            });

        }
    }
}
