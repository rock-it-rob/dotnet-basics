﻿// <auto-generated />
using System;
using EntityFrameworkBasics.Notify.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EntityFrameworkBasics.Migrations
{
    [DbContext(typeof(NotificationContext))]
    [Migration("20230321163401_RelationshipChange")]
    partial class RelationshipChange
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EntityFrameworkBasics.Notification.Data.Notification", b =>
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

                    b.Property<DateTime?>("Updated")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated")
                        .HasDefaultValueSql("now()");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_notifications");

                    b.ToTable("notifications", null, t =>
                        {
                            t.HasTrigger("create trigger n1_trigger before insert or update on notifications\n	                        for each row execute procedure set_update()");
                        });
                });

            modelBuilder.Entity("EntityFrameworkBasics.Notification.Data.NotificationMessage", b =>
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

                    b.Property<long?>("NotificationId")
                        .IsRequired()
                        .HasColumnType("bigint")
                        .HasColumnName("notification_id");

                    b.Property<DateTime?>("Updated")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated")
                        .HasDefaultValueSql("now()");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_notification_messages");

                    b.HasIndex("NotificationId")
                        .HasDatabaseName("ix_notification_messages_notification_id");

                    b.ToTable("notification_messages", (string)null);
                });

            modelBuilder.Entity("EntityFrameworkBasics.Notification.Data.NotificationRecipient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email_address");

                    b.Property<long?>("NotificationId")
                        .IsRequired()
                        .HasColumnType("bigint")
                        .HasColumnName("notification_id");

                    b.Property<DateTime?>("Updated")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated")
                        .HasDefaultValueSql("now()");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_notification_recipients");

                    b.HasIndex("NotificationId")
                        .HasDatabaseName("ix_notification_recipients_notification_id");

                    b.ToTable("notification_recipients", (string)null);
                });

            modelBuilder.Entity("EntityFrameworkBasics.Notification.Data.NotificationMessage", b =>
                {
                    b.HasOne("EntityFrameworkBasics.Notification.Data.Notification", "Notification")
                        .WithMany()
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_notification_messages_notifications_notification_id");

                    b.Navigation("Notification");
                });

            modelBuilder.Entity("EntityFrameworkBasics.Notification.Data.NotificationRecipient", b =>
                {
                    b.HasOne("EntityFrameworkBasics.Notification.Data.Notification", "notification")
                        .WithMany()
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_notification_recipients_notifications_notification_id");

                    b.Navigation("notification");
                });
#pragma warning restore 612, 618
        }
    }
}
