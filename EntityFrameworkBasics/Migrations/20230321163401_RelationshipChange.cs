using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkBasics.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated",
                table: "notifications",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated",
                table: "notification_messages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.Sql("drop trigger notifications_setupdate_trg on notifications");
            migrationBuilder.Sql("drop trigger notification_messages_setupdate_trg on notification_messages");

            migrationBuilder.Sql("drop function set_update();");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                create function set_update() returns trigger as
                $$
                begin
                    new.updated = now();
                    return new;
                end;
                $$ language plpgsql;
                """
            );

            migrationBuilder.Sql(
                """
                create trigger notification_messages_setupdate_trg before insert or update on notification_messages
                    for each row execute procedure set_update();
                """
            );

            migrationBuilder.Sql(
                """
                create trigger notifications_setupdate_trg before insert or update on notifications
                    for each row execute procedure set_update();
                """
            );

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated",
                table: "notifications",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated",
                table: "notification_messages",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");
        }
    }
}
