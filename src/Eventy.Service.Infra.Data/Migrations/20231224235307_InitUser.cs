using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventy.Service.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "identity",
                table: "user",
                columns: new[] { "id", "created_at", "created_by", "deleted_at", "deleted_by", "email", "name", "password", "role", "status", "updated_at", "updated_by" },
                values: new object[] { new Guid("2afab1d9-5364-425e-8080-1e237590e5a0"), new DateTime(2023, 12, 24, 20, 53, 7, 320, DateTimeKind.Local).AddTicks(7873), new Guid("b807b9cf-3f3f-4e1b-a4cd-3ee704acc746"), null, null, "admin@eventy.com", "Administrator", "Pwd@123", (short)1, (short)1, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "identity",
                table: "user",
                keyColumn: "id",
                keyValue: new Guid("2afab1d9-5364-425e-8080-1e237590e5a0"));
        }
    }
}
