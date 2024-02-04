using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LawSearchEngine.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Initial_setup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleWithPermission",
                columns: table => new
                {
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleWithPermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RoleWithPermission_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleWithPermission_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Governments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Governments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWithRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWithRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserWithRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWithRole_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Deleted", "Name", "RowVersion" },
                values: new object[,]
                {
                    { 1L, false, "AddRole", new byte[0] },
                    { 2L, false, "EditRole", new byte[0] },
                    { 3L, false, "DeleteRole", new byte[0] },
                    { 4L, false, "AddPaymentOptions", new byte[0] },
                    { 5L, false, "EditPaymentOptions", new byte[0] },
                    { 6L, false, "DeletePaymentOptions", new byte[0] }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Deleted", "Name", "RowVersion" },
                values: new object[,]
                {
                    { 1L, false, "Admin", new byte[0] },
                    { 2L, false, "Agency", new byte[0] }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Deleted", "Password", "RowVersion", "Username" },
                values: new object[] { new Guid("10000000-0000-0000-0000-000000000000"), false, "admin", new byte[0], "admin" });

            migrationBuilder.InsertData(
                table: "RoleWithPermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 2L, 1L },
                    { 3L, 1L },
                    { 4L, 1L },
                    { 5L, 1L },
                    { 6L, 1L }
                });

            migrationBuilder.InsertData(
                table: "UserWithRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1L, new Guid("10000000-0000-0000-0000-000000000000") });

            migrationBuilder.CreateIndex(
                name: "IX_Governments_UserId",
                table: "Governments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleWithPermission_PermissionId",
                table: "RoleWithPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWithRole_RoleId",
                table: "UserWithRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Governments");

            migrationBuilder.DropTable(
                name: "RoleWithPermission");

            migrationBuilder.DropTable(
                name: "UserWithRole");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
