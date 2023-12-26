using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventy.Service.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnableLegacyTimestampBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: new Guid("9e04ff65-eb6d-4fba-a0ca-fdf9b2945785"));

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "created_at", "created_by", "deleted_at", "deleted_by", "email", "name", "password", "role", "status", "updated_at", "updated_by" },
                values: new object[] { new Guid("d5b25a5f-f6ac-46f6-bfcd-2df77f1f0d31"), new DateTime(2023, 12, 25, 20, 57, 1, 451, DateTimeKind.Local).AddTicks(3980), new Guid("a3797fd8-8221-42bf-9d85-a00345488623"), null, null, "admin@eventy.com", "Administrator", "Pwd@123", (short)1, (short)1, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: new Guid("d5b25a5f-f6ac-46f6-bfcd-2df77f1f0d31"));

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "created_at", "created_by", "deleted_at", "deleted_by", "email", "name", "password", "role", "status", "updated_at", "updated_by" },
                values: new object[] { new Guid("9e04ff65-eb6d-4fba-a0ca-fdf9b2945785"), new DateTime(2023, 12, 25, 20, 52, 37, 856, DateTimeKind.Local).AddTicks(4752), new Guid("1cf41953-4798-4e53-af5b-84d866d732df"), null, null, "admin@eventy.com", "Administrator", "Pwd@123", (short)1, (short)1, null, null });
        }
    }
}
