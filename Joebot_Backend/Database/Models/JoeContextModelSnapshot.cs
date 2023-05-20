﻿// <auto-generated />
using System;
using Joebot_Backend.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Joebot_Backend.Database.Models
{
    [DbContext(typeof(JoeContext))]
    partial class JoeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Joebot_Backend.Database.Models.Configuration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DefaultChannel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EnableKickCache")
                        .HasColumnType("bit");

                    b.Property<int>("KickCacheDays")
                        .HasColumnType("int");

                    b.Property<int>("KickCacheHours")
                        .HasColumnType("int");

                    b.Property<string>("KickCacheServerMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KickServerMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KickUserMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("Joebot_Backend.Database.Models.ReactEmote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TriggerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TriggerId");

                    b.ToTable("ReactEmote");
                });

            modelBuilder.Entity("Joebot_Backend.Database.Models.StatusMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ConfigurationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConfigurationId");

                    b.ToTable("StatusMessages");
                });

            modelBuilder.Entity("Joebot_Backend.Database.Models.Trigger", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ConfigurationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IgnoreCooldown")
                        .HasColumnType("bit");

                    b.Property<bool>("MessageDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("SendRandomResponse")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ConfigurationId");

                    b.ToTable("Triggers");
                });

            modelBuilder.Entity("Joebot_Backend.Database.Models.TriggerResponse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TriggerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TriggerId");

                    b.ToTable("TriggerResponses");
                });

            modelBuilder.Entity("Joebot_Backend.Database.Models.TriggerWord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TriggerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TriggerId");

                    b.ToTable("TriggerWords");
                });

            modelBuilder.Entity("Joebot_Backend.Database.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ConfigurationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiscordUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSecert")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConfigurationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Joebot_Backend.Database.Models.ReactEmote", b =>
                {
                    b.HasOne("Joebot_Backend.Database.Models.Trigger", "Trigger")
                        .WithMany("ReactEmotes")
                        .HasForeignKey("TriggerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trigger");
                });

            modelBuilder.Entity("Joebot_Backend.Database.Models.StatusMessage", b =>
                {
                    b.HasOne("Joebot_Backend.Database.Models.Configuration", "Configuration")
                        .WithMany("StatusMessages")
                        .HasForeignKey("ConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Configuration");
                });

            modelBuilder.Entity("Joebot_Backend.Database.Models.Trigger", b =>
                {
                    b.HasOne("Joebot_Backend.Database.Models.Configuration", "Configuration")
                        .WithMany("Triggers")
                        .HasForeignKey("ConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Configuration");
                });

            modelBuilder.Entity("Joebot_Backend.Database.Models.TriggerResponse", b =>
                {
                    b.HasOne("Joebot_Backend.Database.Models.Trigger", "Trigger")
                        .WithMany("TriggerResponses")
                        .HasForeignKey("TriggerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trigger");
                });

            modelBuilder.Entity("Joebot_Backend.Database.Models.TriggerWord", b =>
                {
                    b.HasOne("Joebot_Backend.Database.Models.Trigger", "Trigger")
                        .WithMany("TriggerWords")
                        .HasForeignKey("TriggerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trigger");
                });

            modelBuilder.Entity("Joebot_Backend.Database.Models.User", b =>
                {
                    b.HasOne("Joebot_Backend.Database.Models.Configuration", "Configuration")
                        .WithMany("Users")
                        .HasForeignKey("ConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Configuration");
                });

            modelBuilder.Entity("Joebot_Backend.Database.Models.Configuration", b =>
                {
                    b.Navigation("StatusMessages");

                    b.Navigation("Triggers");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Joebot_Backend.Database.Models.Trigger", b =>
                {
                    b.Navigation("ReactEmotes");

                    b.Navigation("TriggerResponses");

                    b.Navigation("TriggerWords");
                });
#pragma warning restore 612, 618
        }
    }
}
