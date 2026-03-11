using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weardian.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangeKeyBytesPropertToCiphertext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KeyBytes",
                table: "SymmetricKeys",
                newName: "Ciphertext");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ciphertext",
                table: "SymmetricKeys",
                newName: "KeyBytes");
        }
    }
}
