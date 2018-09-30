using SoloLearn.Chat.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoloLearn.Chat.Core.Data
{
    /// <summary>
    /// Basic DbContext
    /// </summary>
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RoomMessages> RoomMessages { get; set; }
        public DbSet<RoomUsers> RoomUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Message>().Property(m => m.CreatedDate);

            builder.Entity<RoomMessages>().HasKey(t => new { t.RoomId, t.MessageId });
            builder.Entity<RoomUsers>().HasKey(t => new { t.RoomId, t.UserId });

            builder.Entity<RoomMessages>()
                .HasOne(t => t.Room)
                .WithMany(t => t.Messages)
                .HasForeignKey(t => t.RoomId);

            builder.Entity<RoomMessages>()
                .HasOne(t => t.Message)
                .WithMany(t => t.Rooms)
                .HasForeignKey(t => t.MessageId);


            builder.Entity<RoomUsers>()
                .HasOne(t => t.Room)
                .WithMany(t => t.Users)
                .HasForeignKey(t => t.UserId);

            builder.Entity<RoomUsers>()
                .HasOne(t => t.User)
                .WithMany(t => t.Rooms)
                .HasForeignKey(t => t.RoomId);
        }
    }
}
