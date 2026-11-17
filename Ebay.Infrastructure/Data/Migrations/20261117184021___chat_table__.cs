using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ebay.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class __chat_table__ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chats",
                schema: "prd",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    sender_id = table.Column<Guid>(type: "uuid", nullable: false),
                    recipient_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    message = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chats", x => x.id);
                    table.ForeignKey(
                        name: "fk_chats_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "prd",
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_chats_user_recipient_id",
                        column: x => x.recipient_id,
                        principalSchema: "usr",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_chats_user_sender_id",
                        column: x => x.sender_id,
                        principalSchema: "usr",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_chats_product_id",
                schema: "prd",
                table: "chats",
                column: "product_id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chats",
                schema: "prd");
        }
    }
}
