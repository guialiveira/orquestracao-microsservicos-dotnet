using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VollMed.Consultas.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "consultas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<long>(type: "bigint", nullable: false),
                    PacienteNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicoId = table.Column<long>(type: "bigint", nullable: false),
                    MedicoNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consultas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "consultas");
        }
    }
}
