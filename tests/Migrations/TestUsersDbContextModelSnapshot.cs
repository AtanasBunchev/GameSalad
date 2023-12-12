﻿// <auto-generated />
using System;
using GameSaladTests.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameSaladTests.Migrations
{
    [DbContext(typeof(TestUsersDbContext))]
    partial class TestUsersDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("GameSalad.Entities.GameEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("Active")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Data")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("GameSalad.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GameSalad.Entities.UserFollowEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FollowerId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TargetId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FollowerId");

                    b.HasIndex("TargetId");

                    b.ToTable("UserFollowEntries");
                });

            modelBuilder.Entity("GameSalad.Entities.GameEntry", b =>
                {
                    b.HasOne("GameSalad.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GameSalad.Entities.UserFollowEntry", b =>
                {
                    b.HasOne("GameSalad.Entities.User", "Follower")
                        .WithMany("Followed")
                        .HasForeignKey("FollowerId");

                    b.HasOne("GameSalad.Entities.User", "Target")
                        .WithMany("Followers")
                        .HasForeignKey("TargetId");

                    b.Navigation("Follower");

                    b.Navigation("Target");
                });

            modelBuilder.Entity("GameSalad.Entities.User", b =>
                {
                    b.Navigation("Followed");

                    b.Navigation("Followers");
                });
#pragma warning restore 612, 618
        }
    }
}
