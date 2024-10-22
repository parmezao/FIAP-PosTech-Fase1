using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contatos.Web.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ValueObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Contato",
                type: "varchar(60)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(80)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Contato",
                type: "varchar(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(60)");
        }
    }
}
