using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contatos.Web.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_DDD_Field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DDD",
                table: "Contato",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DDD",
                table: "Contato");
        }
    }
}
