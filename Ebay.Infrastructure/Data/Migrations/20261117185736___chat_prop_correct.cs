using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ebay.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class __chat_prop_correct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_chats_recipient_id",
                schema: "prd",
                table: "chats");

            migrationBuilder.DropIndex(
                name: "ix_chats_sender_id",
                schema: "prd",
                table: "chats");

            migrationBuilder.CreateIndex(
                name: "ix_chats_recipient_id",
                schema: "prd",
                table: "chats",
                column: "recipient_id");

            migrationBuilder.CreateIndex(
                name: "ix_chats_sender_id",
                schema: "prd",
                table: "chats",
                column: "sender_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_chats_recipient_id",
                schema: "prd",
                table: "chats");

            migrationBuilder.DropIndex(
                name: "ix_chats_sender_id",
                schema: "prd",
                table: "chats");

            migrationBuilder.CreateIndex(
                name: "ix_chats_recipient_id",
                schema: "prd",
                table: "chats",
                column: "recipient_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_chats_sender_id",
                schema: "prd",
                table: "chats",
                column: "sender_id",
                unique: true);
        }
    }
}
