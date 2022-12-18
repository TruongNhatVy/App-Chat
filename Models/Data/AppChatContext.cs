﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models.Models;

namespace Models.Data
{
    public partial class AppChatContext : DbContext
    {
        public AppChatContext()
        {
        }

        public AppChatContext(DbContextOptions<AppChatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<MessageGroup> MessageGroup { get; set; }
        public virtual DbSet<MessageUser> MessageUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-RIDSNNE\\SQLEXPRESS;Initial Catalog=AppChat;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.FullName).IsRequired();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasMany(d => d.Group)
                    .WithMany(p => p.Account)
                    .UsingEntity<Dictionary<string, object>>(
                        "AccountGroup",
                        l => l.HasOne<Group>().WithMany().HasForeignKey("GroupId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Account_Group_Group"),
                        r => r.HasOne<Account>().WithMany().HasForeignKey("AccountId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Account_Group_Account"),
                        j =>
                        {
                            j.HasKey("AccountId", "GroupId");

                            j.ToTable("Account_Group");
                        });
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<MessageGroup>(entity =>
            {
                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.TimeSend).HasColumnType("datetime");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.MessageGroup)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MessageGroup_Group");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageGroup)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MessageGroup_Account");
            });

            modelBuilder.Entity<MessageUser>(entity =>
            {
                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.TimeSend).HasColumnType("datetime");

                entity.HasOne(d => d.Receive)
                    .WithMany(p => p.MessageUserReceive)
                    .HasForeignKey(d => d.ReceiveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MessageUser_Account1");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageUserSender)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MessageUser_Account");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}