using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetDiverse.Data.Migrations
{
    /// <inheritdoc />
    public partial class AcertarRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PessoaDoadora_Cidade_IdCidade",
                table: "PessoaDoadora");

            migrationBuilder.DropForeignKey(
                name: "FK_PessoaDoadora_Estado_IdEstado",
                table: "PessoaDoadora");

            migrationBuilder.DropIndex(
                name: "IX_PessoaDoadora_IdCidade",
                table: "PessoaDoadora");

            migrationBuilder.DropIndex(
                name: "IX_PessoaDoadora_IdEstado",
                table: "PessoaDoadora");

            migrationBuilder.DropColumn(
                name: "IdCidade",
                table: "PessoaDoadora");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "PessoaDoadora");

            migrationBuilder.AddColumn<int>(
                name: "IdEstado",
                table: "Cidade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCidade",
                table: "Bairro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cidade_IdEstado",
                table: "Cidade",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Bairro_IdCidade",
                table: "Bairro",
                column: "IdCidade");

            migrationBuilder.AddForeignKey(
                name: "FK_Bairro_Cidade_IdCidade",
                table: "Bairro",
                column: "IdCidade",
                principalTable: "Cidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cidade_Estado_IdEstado",
                table: "Cidade",
                column: "IdEstado",
                principalTable: "Estado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bairro_Cidade_IdCidade",
                table: "Bairro");

            migrationBuilder.DropForeignKey(
                name: "FK_Cidade_Estado_IdEstado",
                table: "Cidade");

            migrationBuilder.DropIndex(
                name: "IX_Cidade_IdEstado",
                table: "Cidade");

            migrationBuilder.DropIndex(
                name: "IX_Bairro_IdCidade",
                table: "Bairro");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Cidade");

            migrationBuilder.DropColumn(
                name: "IdCidade",
                table: "Bairro");

            migrationBuilder.AddColumn<int>(
                name: "IdCidade",
                table: "PessoaDoadora",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdEstado",
                table: "PessoaDoadora",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PessoaDoadora_IdCidade",
                table: "PessoaDoadora",
                column: "IdCidade");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaDoadora_IdEstado",
                table: "PessoaDoadora",
                column: "IdEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaDoadora_Cidade_IdCidade",
                table: "PessoaDoadora",
                column: "IdCidade",
                principalTable: "Cidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaDoadora_Estado_IdEstado",
                table: "PessoaDoadora",
                column: "IdEstado",
                principalTable: "Estado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
