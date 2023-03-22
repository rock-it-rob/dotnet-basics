using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkBasics.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_notification_messages_notification_id",
                table: "notification_messages");

            migrationBuilder.CreateIndex(
                name: "ix_notification_messages_notification_id",
                table: "notification_messages",
                column: "notification_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_notification_messages_notification_id",
                table: "notification_messages");

            migrationBuilder.CreateIndex(
                name: "ix_notification_messages_notification_id",
                table: "notification_messages",
                column: "notification_id");
        }
    }
}
