
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

namespace EntityFrameworkBasics.Migrations;

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