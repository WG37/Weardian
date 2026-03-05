using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weardian.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SymmetricKeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KeyBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    EnvelopeVersion = table.Column<int>(type: "int", nullable: false),
                    WrapAlgorithm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WrappingKeyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tag = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Nonce = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Ciphertext = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    KeyType = table.Column<int>(type: "int", nullable: false),
                    KeyStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymmetricKeys", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SymmetricKeys_PublicId",
                table: "SymmetricKeys",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SymmetricKeys_WrappingKeyId",
                table: "SymmetricKeys",
                column: "WrappingKeyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SymmetricKeys");
        }
    }
}
