using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weardian.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangePublicIdToEnvelopeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "SymmetricKeyRecords",
                newName: "EnvelopeId");

            migrationBuilder.RenameIndex(
                name: "IX_SymmetricKeyRecords_PublicId",
                table: "SymmetricKeyRecords",
                newName: "IX_SymmetricKeyRecords_EnvelopeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnvelopeId",
                table: "SymmetricKeyRecords",
                newName: "PublicId");

            migrationBuilder.RenameIndex(
                name: "IX_SymmetricKeyRecords_EnvelopeId",
                table: "SymmetricKeyRecords",
                newName: "IX_SymmetricKeyRecords_PublicId");
        }
    }
}
