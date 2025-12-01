using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicos.ServiceAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPacienteUniqueIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "consultas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Paciente = table.Column<string>(type: "TEXT", nullable: false),
                    MedicoId = table.Column<long>(type: "INTEGER", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consultas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_consultas_medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pacientes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Cpf = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pacientes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_medicos_Crm",
                table: "medicos",
                column: "Crm",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_medicos_Email",
                table: "medicos",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_consultas_MedicoId",
                table: "consultas",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_pacientes_Cpf",
                table: "pacientes",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pacientes_Email",
                table: "pacientes",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "consultas");

            migrationBuilder.DropTable(
                name: "pacientes");

            migrationBuilder.DropIndex(
                name: "IX_medicos_Crm",
                table: "medicos");

            migrationBuilder.DropIndex(
                name: "IX_medicos_Email",
                table: "medicos");
        }
    }
}
