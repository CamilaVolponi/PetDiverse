using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetDiverse.Data.Migrations
{
    /// <inheritdoc />
    public partial class AcertarRelacionamento1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "PessoaDoadora");

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "PessoaDoadora");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "PessoaDoadora");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "PessoaDoadora");

            migrationBuilder.DropColumn(
                name: "RedeSocial",
                table: "PessoaDoadora");

            migrationBuilder.DropColumn(
                name: "Site",
                table: "PessoaDoadora");

            migrationBuilder.DropColumn(
                name: "Sobrenome",
                table: "PessoaDoadora");

            migrationBuilder.CreateTable(
                name: "PessoaFisica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaFisica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PessoaFisica_PessoaDoadora_Id",
                        column: x => x.Id,
                        principalTable: "PessoaDoadora",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PessoaJuridica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RedeSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Site = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaJuridica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PessoaJuridica_PessoaDoadora_Id",
                        column: x => x.Id,
                        principalTable: "PessoaDoadora",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PessoaFisica");

            migrationBuilder.DropTable(
                name: "PessoaJuridica");

            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "PessoaDoadora",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "PessoaDoadora",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "PessoaDoadora",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "PessoaDoadora",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RedeSocial",
                table: "PessoaDoadora",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Site",
                table: "PessoaDoadora",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sobrenome",
                table: "PessoaDoadora",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
