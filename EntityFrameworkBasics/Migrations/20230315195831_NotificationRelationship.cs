using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkBasics.Migrations
{
    /// <inheritdoc />
    public partial class NotificationRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "notification_id",
                table: "notification_messages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "ix_notification_messages_notification_id",
                table: "notification_messages",
                column: "notification_id");

            migrationBuilder.AddForeignKey(
                name: "fk_notification_messages_notifications_notification_id",
                table: "notification_messages",
                column: "notification_id",
                principalTable: "notifications",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_notification_messages_notifications_notification_id",
                table: "notification_messages");

            migrationBuilder.DropIndex(
                name: "ix_notification_messages_notification_id",
                table: "notification_messages");

            migrationBuilder.DropColumn(
                name: "notification_id",
                table: "notification_messages");
        }
    }
}
