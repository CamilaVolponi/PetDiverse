using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetDiverse.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSiglaEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sigla",
                table: "Estado",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sigla",
                table: "Estado");
        }
    }
}
