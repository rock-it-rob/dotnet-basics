using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EntityFrameworkBasics.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "notification_messages",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    message = table.Column<string>(type: "text", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notification_messages", x => x.id);
                });

            migrationBuilder.Sql(
                """
                create trigger notification_messages_setupdate_trg before insert or update on notification_messages
                    for each row execute procedure set_update();
                """
            );

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    subject = table.Column<string>(type: "text", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notifications", x => x.id);
                });

            migrationBuilder.Sql(
                """
                create trigger notifications_setupdate_trg before insert or update on notifications
                    for each row execute procedure set_update();
                """
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notification_messages");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.Sql("drop function set_update();");
        }
    }
}
