using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
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
            migrationBuilder.CreateSetUpdateFunction();

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

            migrationBuilder.AddSetUpdateTrigger("notification_messages");

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

            migrationBuilder.AddSetUpdateTrigger("notifications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notification_messages");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropSetUpdateFunction();
        }
    }

    public static class Extensions
    {
        public static OperationBuilder<SqlOperation> CreateSetUpdateFunction(this MigrationBuilder migrationBuilder)
            =>
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

        public static OperationBuilder<SqlOperation> DropSetUpdateFunction(this MigrationBuilder migrationBuilder)
            => migrationBuilder.Sql("drop function set_update();");

        public static OperationBuilder<SqlOperation> AddSetUpdateTrigger(this MigrationBuilder migrationBuilder, string table)
            =>
            migrationBuilder.Sql(
                $"""
                create trigger {table}_setupdate_trg before insert or update on {table}
                    for each row execute procedure set_update();
                """
            );
    }
}
