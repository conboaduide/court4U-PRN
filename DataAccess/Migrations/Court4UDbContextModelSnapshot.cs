﻿// <auto-generated />
using System;
using DataAccess.Entity;
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

            modelBuilder.Entity("DataAccess.Entity.Data.Bill", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Bill");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.BookedSlot", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("CheckedIn")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SlotId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SlotId");

                    b.ToTable("BookedSlot");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.Booking", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.Club", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CityOfProvince")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

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

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.ClubImage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClubId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClubImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.ToTable("ClubImage");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.Court", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClubId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Num")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.ToTable("Court");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.MemberSubscription", b =>
                {
                    b.Property<string>("MemberId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubscriptionOptionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("MemberId");

                    b.HasIndex("SubscriptionOptionId");

                    b.ToTable("MemberSubscription");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.Review", b =>
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

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("ReviewerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.HasIndex("ReviewerId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.Slot", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClubId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfWeek")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.ToTable("Slot");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.StaffProfile", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClubId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.ToTable("StaffProfile");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.SubOptionSlot", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SlotId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SubscriptionOptionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SlotId");

                    b.HasIndex("SubscriptionOptionId");

                    b.ToTable("SubOptionSlot");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.SubscriptionOption", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClubId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.ToTable("SubscriptionOption");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

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

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.BookedSlot", b =>
                {
                    b.HasOne("DataAccess.Entity.Data.Slot", "Slot")
                        .WithMany("BookedSlots")
                        .HasForeignKey("SlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Slot");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.Booking", b =>
                {
                    b.HasOne("DataAccess.Entity.Data.Bill", "Bill")
                        .WithOne("Booking")
                        .HasForeignKey("DataAccess.Entity.Data.Booking", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Entity.Data.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.Club", b =>
                {
                    b.HasOne("DataAccess.Entity.Data.User", "User")
                        .WithMany("Clubs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.ClubImage", b =>
                {
                    b.HasOne("DataAccess.Entity.Data.Club", "Club")
                        .WithMany("ClubImages")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.Court", b =>
                {
                    b.HasOne("DataAccess.Entity.Data.Club", "Club")
                        .WithMany("Courts")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.MemberSubscription", b =>
                {
                    b.HasOne("DataAccess.Entity.Data.Bill", "Bill")
                        .WithOne("MemberSubscription")
                        .HasForeignKey("DataAccess.Entity.Data.MemberSubscription", "MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Entity.Data.User", "Member")
                        .WithMany("MemberSubscriptions")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Entity.Data.SubscriptionOption", "SubscriptionOption")
                        .WithMany("MemberSubscriptions")
                        .HasForeignKey("SubscriptionOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Member");

                    b.Navigation("SubscriptionOption");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.Review", b =>
                {
                    b.HasOne("DataAccess.Entity.Data.Club", "Club")
                        .WithMany("Reviews")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Entity.Data.User", "Reviewer")
                        .WithMany("Reviews")
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Club");

                    b.Navigation("Reviewer");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.Slot", b =>
                {
                    b.HasOne("DataAccess.Entity.Data.Club", "Club")
                        .WithMany("Slots")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.StaffProfile", b =>
                {
                    b.HasOne("DataAccess.Entity.Data.Club", "Club")
                        .WithMany("StaffProfiles")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Entity.Data.User", "User")
                        .WithOne("StaffProfile")
                        .HasForeignKey("DataAccess.Entity.Data.StaffProfile", "Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Club");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.SubOptionSlot", b =>
                {
                    b.HasOne("DataAccess.Entity.Data.Slot", "Slot")
                        .WithMany("SubOptionSlots")
                        .HasForeignKey("SlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Entity.Data.SubscriptionOption", "SubscriptionOption")
                        .WithMany("SubOptionSlots")
                        .HasForeignKey("SubscriptionOptionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Slot");

                    b.Navigation("SubscriptionOption");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.SubscriptionOption", b =>
                {
                    b.HasOne("DataAccess.Entity.Data.Club", "Club")
                        .WithMany("SubscriptionOptions")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.Bill", b =>
                {
                    b.Navigation("Booking")
                        .IsRequired();

                    b.Navigation("MemberSubscription")
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Entity.Data.Club", b =>
                {
                    b.Navigation("ClubImages");

                    b.Navigation("Courts");

                    b.Navigation("Reviews");

                    b.Navigation("Slots");

                    b.Navigation("StaffProfiles");

                    b.Navigation("SubscriptionOptions");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.Slot", b =>
                {
                    b.Navigation("BookedSlots");

                    b.Navigation("SubOptionSlots");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.SubscriptionOption", b =>
                {
                    b.Navigation("MemberSubscriptions");

                    b.Navigation("SubOptionSlots");
                });

            modelBuilder.Entity("DataAccess.Entity.Data.User", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Clubs");

                    b.Navigation("MemberSubscriptions");

                    b.Navigation("Reviews");

                    b.Navigation("StaffProfile")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
