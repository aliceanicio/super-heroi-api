using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace super_heroi_api.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirColunasSuperpoderes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Superpoderes");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Superpoderes",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Superpoder",
                table: "Superpoderes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Superpoderes");

            migrationBuilder.DropColumn(
                name: "Superpoder",
                table: "Superpoderes");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Superpoderes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
