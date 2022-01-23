using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginRazorBlog.Infrastructure.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    LastLoginTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastLoginIp = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    CreateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalAccount", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "LocalAccount",
                columns: new[] { "Id", "CreateTimeUtc", "LastLoginIp", "LastLoginTimeUtc", "PasswordHash", "Username" },
                values: new object[] { new Guid("ca421bc6-b9b8-408c-a7a7-1b7a62fcdaa1"), new DateTime(2022, 1, 23, 6, 34, 32, 871, DateTimeKind.Local).AddTicks(2527), null, null, "+ObKUZdjjK7KFGia3w7kAEIOEtQbhy+SGGHhsMQQJfs=", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalAccount");
        }
    }
}
