using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AppData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b73a3b0d-1644-40ff-b881-b07c6843e13b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da7321af-4ef5-4276-948e-90b16aa5110b");

            migrationBuilder.CreateTable(
                name: "HiddenBankAccounts",
                columns: table => new
                {
                    BankAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiddenBankAccounts", x => x.BankAccountId);
                    table.ForeignKey(
                        name: "FK_HiddenBankAccounts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "51920500-f63f-40ab-8f76-78641bd776cd", null, "Employee", "EMPLOYEE" },
                    { "a0b6c991-7747-44a3-b707-5a6835ca4c55", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HiddenBankAccounts_UserId",
                table: "HiddenBankAccounts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HiddenBankAccounts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51920500-f63f-40ab-8f76-78641bd776cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0b6c991-7747-44a3-b707-5a6835ca4c55");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b73a3b0d-1644-40ff-b881-b07c6843e13b", null, "User", "USER" },
                    { "da7321af-4ef5-4276-948e-90b16aa5110b", null, "Employee", "EMPLOYEE" }
                });
        }
    }
}
