using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using Microsoft.Extensions.Configuration;
using System;

namespace DataAccess.Data
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
        public DbSet<BookedSlot> BookedSlots { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Cancellation> Cancellations { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<ClubImage> ClubImages { get; set; }
        public DbSet<ClubRole> ClubRoles { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<MemberSubscription> MemberSubscriptions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Pricing> Pricings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<StaffProfile> StaffProfiles { get; set; }
        public DbSet<StaffRole> StaffRoles { get; set; }
        public DbSet<SubOptionSlot> SubOptionSlots { get; set; }
        public DbSet<SubscriptionOption> SubscriptionOptions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Court4U")) 
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Local"),
                options => options.EnableRetryOnFailure());
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
            modelBuilder.Entity<BookedSlot>()
                .HasKey(bs => bs.Id);
            modelBuilder.Entity<BookedSlot>()
                .HasOne(bs => bs.Slot)
                .WithMany(s => s.BookedSlots)
                .HasForeignKey(bs => bs.SlotId);
            modelBuilder.Entity<BookedSlot>()
                .HasOne(bs => bs.Cancellation)
                .WithOne(c => c.BookedSlot)
                .HasForeignKey<Cancellation>(c => c.Id);

            //
            modelBuilder.Entity<Booking>()
                .HasKey(b => b.Id);
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);

            //
            modelBuilder.Entity<Cancellation>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Cancellation>()
               .HasOne(c => c.Canceller)
               .WithMany(u => u.Cancellations)
               .HasForeignKey(c => c.CancellerId)
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
            modelBuilder.Entity<ClubRole>()
                .HasKey(cr => cr.Id);
            modelBuilder.Entity<ClubRole>()
                .HasOne(cr => cr.Club)
                .WithMany(c => c.ClubRoles)
                .HasForeignKey(cr => cr.ClubId);
            modelBuilder.Entity<ClubRole>()
                .HasOne(cr => cr.Permission)
                .WithMany(p => p.ClubRoles)
                .HasForeignKey(cr => cr.PermissionId);

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
            modelBuilder.Entity<Permission>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Pricing>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Pricing>()
                .HasOne(p => p.Club)
                .WithMany(c => c.Pricings)
                .HasForeignKey(p => p.ClubId);

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
            modelBuilder.Entity<Role>()
                .HasKey(r => r.Id);

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
            modelBuilder.Entity<StaffRole>()
                .HasKey(sr => sr.Id);
            modelBuilder.Entity<StaffRole>()
                .HasOne(sr => sr.ClubRole)
                .WithMany(cr => cr.StaffRoles)
                .HasForeignKey(sr => sr.ClubRoleId);
            modelBuilder.Entity<StaffRole>()
                .HasOne(sr => sr.User)
                .WithMany(u => u.StaffRoles)
                .HasForeignKey(sr => sr.UserId)
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

            //
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => ur.Id);
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
        }
    }
}
