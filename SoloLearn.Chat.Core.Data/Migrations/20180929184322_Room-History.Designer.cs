﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoloLearn.Chat.Core.Data;

namespace SoloLearn.Chat.Core.Data.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    [Migration("20180929184322_Room-History")]
    partial class RoomHistory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065");

            modelBuilder.Entity("SoloLearn.Chat.Core.Entities.Connection", b =>
                {
                    b.Property<string>("ConnectionID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Connected");

                    b.Property<string>("UserAgent");

                    b.Property<string>("UserId");

                    b.HasKey("ConnectionID");

                    b.HasIndex("UserId");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("SoloLearn.Chat.Core.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int?>("RoomId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("SoloLearn.Chat.Core.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MessageId");

                    b.Property<string>("Name");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.HasIndex("UserId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("SoloLearn.Chat.Core.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("PasswordHash");

                    b.Property<int?>("RoomId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SoloLearn.Chat.Core.Entities.Connection", b =>
                {
                    b.HasOne("SoloLearn.Chat.Core.Entities.User")
                        .WithMany("Connections")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SoloLearn.Chat.Core.Entities.Message", b =>
                {
                    b.HasOne("SoloLearn.Chat.Core.Entities.Room")
                        .WithMany("Messages")
                        .HasForeignKey("RoomId");

                    b.HasOne("SoloLearn.Chat.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SoloLearn.Chat.Core.Entities.Room", b =>
                {
                    b.HasOne("SoloLearn.Chat.Core.Entities.Message")
                        .WithMany("Rooms")
                        .HasForeignKey("MessageId");

                    b.HasOne("SoloLearn.Chat.Core.Entities.User")
                        .WithMany("Rooms")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SoloLearn.Chat.Core.Entities.User", b =>
                {
                    b.HasOne("SoloLearn.Chat.Core.Entities.Room")
                        .WithMany("Users")
                        .HasForeignKey("RoomId");
                });
#pragma warning restore 612, 618
        }
    }
}
