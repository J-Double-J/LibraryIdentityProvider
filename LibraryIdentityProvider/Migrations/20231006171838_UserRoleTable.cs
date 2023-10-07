using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryIdentityProvider.Migrations
{
    /// <inheritdoc />
    public partial class UserRoleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Claims",
                table: "UserAccount");

            migrationBuilder.AddColumn<Guid>(
                name: "UserAccountId",
                table: "Role",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RoleID, x.UserID });
                    table.ForeignKey(
                        name: "FK_RoleUser_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_UserAccount_UserID",
                        column: x => x.UserID,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Role_UserAccountId",
                table: "Role",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UserID",
                table: "RoleUser",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_UserAccount_UserAccountId",
                table: "Role",
                column: "UserAccountId",
                principalTable: "UserAccount",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_UserAccount_UserAccountId",
                table: "Role");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropIndex(
                name: "IX_Role_UserAccountId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "Role");

            migrationBuilder.AddColumn<string[]>(
                name: "Claims",
                table: "UserAccount",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);
        }
    }
}
