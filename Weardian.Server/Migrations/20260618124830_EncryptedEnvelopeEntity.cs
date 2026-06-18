using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weardian.Server.Migrations
{
    /// <inheritdoc />
    public partial class EncryptedEnvelopeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SymmetricKeyRecords_AspNetUsers_UserId",
                table: "SymmetricKeyRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SymmetricKeyRecords",
                table: "SymmetricKeyRecords");

            migrationBuilder.DropIndex(
                name: "IX_SymmetricKeyRecords_EnvelopeId",
                table: "SymmetricKeyRecords");

            migrationBuilder.DropIndex(
                name: "IX_SymmetricKeyRecords_UserId",
                table: "SymmetricKeyRecords");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SymmetricKeyRecords");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AspNetUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SymmetricKeyRecords",
                table: "SymmetricKeyRecords",
                column: "EnvelopeId");

            migrationBuilder.CreateTable(
                name: "SymmetricEncryptedEnvelopes",
                columns: table => new
                {
                    EnvelopeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymmetricEncryptedEnvelopes", x => x.EnvelopeId);
                    table.ForeignKey(
                        name: "FK_SymmetricEncryptedEnvelopes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SymmetricPayloadRecords",
                columns: table => new
                {
                    EnvelopeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ciphertext = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    EnvelopeVersion = table.Column<int>(type: "int", nullable: false),
                    Algorithm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nonce = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Tag = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    KeyType = table.Column<int>(type: "int", nullable: false),
                    KeyStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymmetricPayloadRecords", x => x.EnvelopeId);
                    table.ForeignKey(
                        name: "FK_SymmetricPayloadRecords_SymmetricEncryptedEnvelopes_EnvelopeId",
                        column: x => x.EnvelopeId,
                        principalTable: "SymmetricEncryptedEnvelopes",
                        principalColumn: "EnvelopeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SymmetricEncryptedEnvelopes_UserId",
                table: "SymmetricEncryptedEnvelopes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SymmetricKeyRecords_SymmetricEncryptedEnvelopes_EnvelopeId",
                table: "SymmetricKeyRecords",
                column: "EnvelopeId",
                principalTable: "SymmetricEncryptedEnvelopes",
                principalColumn: "EnvelopeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SymmetricKeyRecords_SymmetricEncryptedEnvelopes_EnvelopeId",
                table: "SymmetricKeyRecords");

            migrationBuilder.DropTable(
                name: "SymmetricPayloadRecords");

            migrationBuilder.DropTable(
                name: "SymmetricEncryptedEnvelopes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SymmetricKeyRecords",
                table: "SymmetricKeyRecords");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SymmetricKeyRecords",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SymmetricKeyRecords",
                table: "SymmetricKeyRecords",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SymmetricKeyRecords_EnvelopeId",
                table: "SymmetricKeyRecords",
                column: "EnvelopeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SymmetricKeyRecords_UserId",
                table: "SymmetricKeyRecords",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SymmetricKeyRecords_AspNetUsers_UserId",
                table: "SymmetricKeyRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
