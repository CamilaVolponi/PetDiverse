using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetDiverse.Data.Migrations
{
    /// <inheritdoc />
    public partial class AcertoUsuario1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PessoaDoadora_AspNetUsers_UsuariosId",
                table: "PessoaDoadora");

            migrationBuilder.DropIndex(
                name: "IX_PessoaDoadora_UsuariosId",
                table: "PessoaDoadora");

            migrationBuilder.DropColumn(
                name: "UsuariosId",
                table: "PessoaDoadora");

            migrationBuilder.AlterColumn<string>(
                name: "IdUsuario",
                table: "PessoaDoadora",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaDoadora_IdUsuario",
                table: "PessoaDoadora",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaDoadora_AspNetUsers_IdUsuario",
                table: "PessoaDoadora",
                column: "IdUsuario",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PessoaDoadora_AspNetUsers_IdUsuario",
                table: "PessoaDoadora");

            migrationBuilder.DropIndex(
                name: "IX_PessoaDoadora_IdUsuario",
                table: "PessoaDoadora");

            migrationBuilder.AlterColumn<string>(
                name: "IdUsuario",
                table: "PessoaDoadora",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UsuariosId",
                table: "PessoaDoadora",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaDoadora_UsuariosId",
                table: "PessoaDoadora",
                column: "UsuariosId");

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaDoadora_AspNetUsers_UsuariosId",
                table: "PessoaDoadora",
                column: "UsuariosId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
