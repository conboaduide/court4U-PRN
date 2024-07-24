using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using DataAccess.Entity.Data;

namespace DataAccess.Entity
{
    public interface ICourt4UDbContext
    {

    }
    public class Court4UDbContext : DbContext, ICourt4UDbContext
    {
        public Court4UDbContext() { }
        public Court4UDbContext(DbContextOptions<Court4UDbContext> options) : base(options)
        { }
        public DbSet<Bill> Bills { get; set; }
        //public DbSet<BookedSlot> BookedSlots { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<ClubImage> ClubImages { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<MemberSubscription> MemberSubscriptions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<StaffProfile> StaffProfiles { get; set; }
        public DbSet<SubOptionSlot> SubOptionSlots { get; set; }
        public DbSet<SubscriptionOption> SubscriptionOptions { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                if (!optionsBuilder.IsConfigured)
                {
                    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    var configuration = new ConfigurationBuilder()
                        .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Court4U"))
                        .AddJsonFile($"appsettings.{environment}.json", optional: true)
                        .AddJsonFile("appsettings.json")
                        .Build();
                    // Log the basePath for debugging purposes
                    Console.WriteLine($"Base Path: {configuration}");
                    optionsBuilder.UseSqlServer(configuration.GetConnectionString("Local"),
                        options => options.EnableRetryOnFailure());
                }
            }
            catch (Exception ex)
            {
                // Log the exception or print to console for debugging
                Console.WriteLine($"Exception in OnConfiguring: {ex.Message}");
                throw; // Rethrow the exception to halt application startup and diagnose the issue
            }

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //
            modelBuilder.Entity<Bill>()
               .HasKey(b => b.Id);
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Booking)
                .WithOne(b => b.Bill)
                .HasForeignKey<Booking>(b => b.Id);

            //
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.MemberSubscription)
                .WithOne(ms => ms.Bill)
                .HasForeignKey<MemberSubscription>(ms => ms.MemberId);

            //
            //modelBuilder.Entity<BookedSlot>()
            //    .HasKey(bs => bs.Id);
            //modelBuilder.Entity<BookedSlot>()
            //    .HasOne(bs => bs.Slot)
            //    .WithMany(s => s.BookedSlots)
            //    .HasForeignKey(bs => bs.SlotId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //
            modelBuilder.Entity<Booking>()
                .HasKey(b => b.Id);
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            //
            modelBuilder.Entity<Club>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Club>()
                .HasOne(c => c.User)
                .WithMany(u => u.Clubs)
                .HasForeignKey(c => c.UserId);

            //
            modelBuilder.Entity<ClubImage>()
                .HasKey(ci => ci.Id);
            modelBuilder.Entity<ClubImage>()
                .HasOne(ci => ci.Club)
                .WithMany(c => c.ClubImages)
                .HasForeignKey(ci => ci.ClubId);


            //
            modelBuilder.Entity<Court>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Court>()
                .HasOne(c => c.Club)
                .WithMany(c => c.Courts)
                .HasForeignKey(c => c.ClubId);

            //
            modelBuilder.Entity<MemberSubscription>()
                .HasKey(ms => ms.MemberId);
            modelBuilder.Entity<MemberSubscription>()
                .HasOne(ms => ms.Member)
                .WithMany(u => u.MemberSubscriptions)
                .HasForeignKey(ms => ms.MemberId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MemberSubscription>()
                .HasOne(ms => ms.SubscriptionOption)
                .WithMany(so => so.MemberSubscriptions)
                .HasForeignKey(ms => ms.SubscriptionOptionId);


            //
            modelBuilder.Entity<Review>()
                .HasKey(r => r.Id);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Club)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.ClubId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Reviewer)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.ReviewerId)
                .OnDelete(DeleteBehavior.NoAction);

            //
            modelBuilder.Entity<Slot>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<Slot>()
                .HasOne(s => s.Club)
                .WithMany(c => c.Slots)
                .HasForeignKey(s => s.ClubId);

            //
            modelBuilder.Entity<StaffProfile>()
                .HasKey(sp => sp.Id);
            modelBuilder.Entity<StaffProfile>()
                .HasOne(sp => sp.Club)
                .WithMany(c => c.StaffProfiles)
                .HasForeignKey(sp => sp.ClubId);
            modelBuilder.Entity<StaffProfile>()
                .HasOne(sp => sp.User)
                .WithOne(u => u.StaffProfile)
                .HasForeignKey<StaffProfile>(sp => sp.Id)
                .OnDelete(DeleteBehavior.NoAction);

            //
            modelBuilder.Entity<SubOptionSlot>()
                .HasKey(sos => sos.Id);
            modelBuilder.Entity<SubOptionSlot>()
                .HasOne(sos => sos.Slot)
                .WithMany(s => s.SubOptionSlots)
                .HasForeignKey(sos => sos.SlotId);
            modelBuilder.Entity<SubOptionSlot>()
                .HasOne(sos => sos.SubscriptionOption)
                .WithMany(so => so.SubOptionSlots)
                .HasForeignKey(sos => sos.SubscriptionOptionId)
                .OnDelete(DeleteBehavior.NoAction);

            //
            modelBuilder.Entity<SubscriptionOption>()
                .HasKey(so => so.Id);
            modelBuilder.Entity<SubscriptionOption>()
                .HasOne(so => so.Club)
                .WithMany(c => c.SubscriptionOptions)
                .HasForeignKey(so => so.ClubId);

            //
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
        }
    }
}
