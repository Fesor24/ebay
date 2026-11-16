using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ebay.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class __user_product_rel_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                schema: "prd",
                table: "products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_products_user_id",
                schema: "prd",
                table: "products",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_products_user_user_id",
                schema: "prd",
                table: "products",
                column: "user_id",
                principalSchema: "usr",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_user_user_id",
                schema: "prd",
                table: "products");

            migrationBuilder.DropIndex(
                name: "ix_products_user_id",
                schema: "prd",
                table: "products");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "prd",
                table: "products");
        }
    }
}
