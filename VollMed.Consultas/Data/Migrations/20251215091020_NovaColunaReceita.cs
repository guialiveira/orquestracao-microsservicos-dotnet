using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VollMed.Consultas.Data.Migrations
{
    /// <inheritdoc />
    public partial class NovaColunaReceita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Receita",
                table: "consultas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Receita",
                table: "consultas");
        }
    }
}
