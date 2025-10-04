using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetDiverse.Data.Migrations
{
    /// <inheritdoc />
    public partial class Acertar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_PessoaDoadora_IdDoador",
                table: "Animal");

            migrationBuilder.RenameColumn(
                name: "IdDoador",
                table: "Animal",
                newName: "IdPessoaDoadora");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_IdDoador",
                table: "Animal",
                newName: "IX_Animal_IdPessoaDoadora");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_PessoaDoadora_IdPessoaDoadora",
                table: "Animal",
                column: "IdPessoaDoadora",
                principalTable: "PessoaDoadora",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_PessoaDoadora_IdPessoaDoadora",
                table: "Animal");

            migrationBuilder.RenameColumn(
                name: "IdPessoaDoadora",
                table: "Animal",
                newName: "IdDoador");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_IdPessoaDoadora",
                table: "Animal",
                newName: "IX_Animal_IdDoador");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_PessoaDoadora_IdDoador",
                table: "Animal",
                column: "IdDoador",
                principalTable: "PessoaDoadora",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
