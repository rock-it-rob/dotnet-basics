﻿// <auto-generated />
using System;
using EntityFrameworkBasics.Data.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EntityFrameworkBasics.Migrations
{
    [DbContext(typeof(NotificationContext))]
    [Migration("20230314201640_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EntityFrameworkBasics.Data.Notification.Notification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("subject");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated");

                    b.HasKey("Id")
                        .HasName("pk_notifications");

                    b.ToTable("notifications", null, t =>
                        {
                            t.HasTrigger("create trigger n1_trigger before insert or update on notifications\n	                        for each row execute procedure set_update()");
                        });
                });

            modelBuilder.Entity("EntityFrameworkBasics.Data.Notification.NotificationMessage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("message");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated");

                    b.HasKey("Id")
                        .HasName("pk_notification_messages");

                    b.ToTable("notification_messages", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}