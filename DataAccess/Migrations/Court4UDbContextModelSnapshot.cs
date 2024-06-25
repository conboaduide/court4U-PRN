﻿// <auto-generated />
using System;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(Court4UDbContext))]
    partial class Court4UDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAccess.Data.Bill", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bill");
                });

            modelBuilder.Entity("DataAccess.Data.BookedSlot", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("CheckedIn")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("SlotId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SlotId");

                    b.ToTable("BookedSlot");
                });

            modelBuilder.Entity("DataAccess.Data.Booking", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("DataAccess.Data.Cancellation", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CancellerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CancellerId");

                    b.ToTable("Cancellation");
                });

            modelBuilder.Entity("DataAccess.Data.Club", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CityOfProvince")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("DataAccess.Data.ClubImage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClubId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClubImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.ToTable("ClubImage");
                });

            modelBuilder.Entity("DataAccess.Data.ClubRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClubId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PermissionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.HasIndex("PermissionId");

                    b.ToTable("ClubRole");
                });

            modelBuilder.Entity("DataAccess.Data.Court", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClubId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Num")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.ToTable("Court");
                });

            modelBuilder.Entity("DataAccess.Data.MemberSubscription", b =>
                {
                    b.Property<string>("MemberId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SubscriptionOptionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MemberId");

                    b.HasIndex("SubscriptionOptionId");

                    b.ToTable("MemberSubscription");
                });

            modelBuilder.Entity("DataAccess.Data.Permission", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("DataAccess.Data.Pricing", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClubId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.ToTable("Pricings");
                });

            modelBuilder.Entity("DataAccess.Data.Review", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClubId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CommentLeft")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommentRight")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("ReviewerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.HasIndex("ReviewerId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("DataAccess.Data.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("DataAccess.Data.Slot", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClubId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateOfWeek")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.ToTable("Slot");
                });

            modelBuilder.Entity("DataAccess.Data.StaffProfile", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClubId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.ToTable("StaffProfile");
                });

            modelBuilder.Entity("DataAccess.Data.StaffRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClubRoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ClubRoleId");

                    b.HasIndex("UserId");

                    b.ToTable("StaffRole");
                });

            modelBuilder.Entity("DataAccess.Data.SubOptionSlot", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SlotId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SubscriptionOptionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SlotId");

                    b.HasIndex("SubscriptionOptionId");

                    b.ToTable("SubOptionSlot");
                });

            modelBuilder.Entity("DataAccess.Data.SubscriptionOption", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClubId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<float>("price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.ToTable("SubscriptionOption");
                });

            modelBuilder.Entity("DataAccess.Data.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataAccess.Data.UserRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MemberSubscriptionMemberId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("MemberSubscriptionMemberId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("DataAccess.Data.BookedSlot", b =>
                {
                    b.HasOne("DataAccess.Data.Slot", "Slot")
                        .WithMany("BookedSlots")
                        .HasForeignKey("SlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Slot");
                });

            modelBuilder.Entity("DataAccess.Data.Booking", b =>
                {
                    b.HasOne("DataAccess.Data.Bill", "Bill")
                        .WithOne("Booking")
                        .HasForeignKey("DataAccess.Data.Booking", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Data.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Data.Cancellation", b =>
                {
                    b.HasOne("DataAccess.Data.User", "Canceller")
                        .WithMany("Cancellations")
                        .HasForeignKey("CancellerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Data.BookedSlot", "BookedSlot")
                        .WithOne("Cancellation")
                        .HasForeignKey("DataAccess.Data.Cancellation", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookedSlot");

                    b.Navigation("Canceller");
                });

            modelBuilder.Entity("DataAccess.Data.Club", b =>
                {
                    b.HasOne("DataAccess.Data.User", "User")
                        .WithMany("Clubs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Data.ClubImage", b =>
                {
                    b.HasOne("DataAccess.Data.Club", "Club")
                        .WithMany("ClubImages")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");
                });

            modelBuilder.Entity("DataAccess.Data.ClubRole", b =>
                {
                    b.HasOne("DataAccess.Data.Club", "Club")
                        .WithMany("ClubRoles")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Data.Permission", "Permission")
                        .WithMany("ClubRoles")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");

                    b.Navigation("Permission");
                });

            modelBuilder.Entity("DataAccess.Data.Court", b =>
                {
                    b.HasOne("DataAccess.Data.Club", "Club")
                        .WithMany("Courts")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");
                });

            modelBuilder.Entity("DataAccess.Data.MemberSubscription", b =>
                {
                    b.HasOne("DataAccess.Data.Bill", "Bill")
                        .WithOne("MemberSubscription")
                        .HasForeignKey("DataAccess.Data.MemberSubscription", "MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Data.User", "Member")
                        .WithMany("MemberSubscriptions")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Data.SubscriptionOption", "SubscriptionOption")
                        .WithMany("MemberSubscriptions")
                        .HasForeignKey("SubscriptionOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Member");

                    b.Navigation("SubscriptionOption");
                });

            modelBuilder.Entity("DataAccess.Data.Pricing", b =>
                {
                    b.HasOne("DataAccess.Data.Club", "Club")
                        .WithMany("Pricings")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");
                });

            modelBuilder.Entity("DataAccess.Data.Review", b =>
                {
                    b.HasOne("DataAccess.Data.Club", "Club")
                        .WithMany("Reviews")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Data.User", "Reviewer")
                        .WithMany("Reviews")
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Club");

                    b.Navigation("Reviewer");
                });

            modelBuilder.Entity("DataAccess.Data.Slot", b =>
                {
                    b.HasOne("DataAccess.Data.Club", "Club")
                        .WithMany("Slots")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");
                });

            modelBuilder.Entity("DataAccess.Data.StaffProfile", b =>
                {
                    b.HasOne("DataAccess.Data.Club", "Club")
                        .WithMany("StaffProfiles")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Data.User", "User")
                        .WithOne("StaffProfile")
                        .HasForeignKey("DataAccess.Data.StaffProfile", "Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Club");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Data.StaffRole", b =>
                {
                    b.HasOne("DataAccess.Data.ClubRole", "ClubRole")
                        .WithMany("StaffRoles")
                        .HasForeignKey("ClubRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Data.User", "User")
                        .WithMany("StaffRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ClubRole");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Data.SubOptionSlot", b =>
                {
                    b.HasOne("DataAccess.Data.Slot", "Slot")
                        .WithMany("SubOptionSlots")
                        .HasForeignKey("SlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Data.SubscriptionOption", "SubscriptionOption")
                        .WithMany("SubOptionSlots")
                        .HasForeignKey("SubscriptionOptionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Slot");

                    b.Navigation("SubscriptionOption");
                });

            modelBuilder.Entity("DataAccess.Data.SubscriptionOption", b =>
                {
                    b.HasOne("DataAccess.Data.Club", "Club")
                        .WithMany("SubscriptionOptions")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");
                });

            modelBuilder.Entity("DataAccess.Data.UserRole", b =>
                {
                    b.HasOne("DataAccess.Data.MemberSubscription", null)
                        .WithMany("UserRoles")
                        .HasForeignKey("MemberSubscriptionMemberId");

                    b.HasOne("DataAccess.Data.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Data.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Data.Bill", b =>
                {
                    b.Navigation("Booking")
                        .IsRequired();

                    b.Navigation("MemberSubscription")
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Data.BookedSlot", b =>
                {
                    b.Navigation("Cancellation")
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Data.Club", b =>
                {
                    b.Navigation("ClubImages");

                    b.Navigation("ClubRoles");

                    b.Navigation("Courts");

                    b.Navigation("Pricings");

                    b.Navigation("Reviews");

                    b.Navigation("Slots");

                    b.Navigation("StaffProfiles");

                    b.Navigation("SubscriptionOptions");
                });

            modelBuilder.Entity("DataAccess.Data.ClubRole", b =>
                {
                    b.Navigation("StaffRoles");
                });

            modelBuilder.Entity("DataAccess.Data.MemberSubscription", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("DataAccess.Data.Permission", b =>
                {
                    b.Navigation("ClubRoles");
                });

            modelBuilder.Entity("DataAccess.Data.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("DataAccess.Data.Slot", b =>
                {
                    b.Navigation("BookedSlots");

                    b.Navigation("SubOptionSlots");
                });

            modelBuilder.Entity("DataAccess.Data.SubscriptionOption", b =>
                {
                    b.Navigation("MemberSubscriptions");

                    b.Navigation("SubOptionSlots");
                });

            modelBuilder.Entity("DataAccess.Data.User", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Cancellations");

                    b.Navigation("Clubs");

                    b.Navigation("MemberSubscriptions");

                    b.Navigation("Reviews");

                    b.Navigation("StaffProfile")
                        .IsRequired();

                    b.Navigation("StaffRoles");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
