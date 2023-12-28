using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventy.Service.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "event",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(200)", nullable: false),
                    description = table.Column<string>(type: "varchar(5000)", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    location = table.Column<string>(type: "varchar(200)", nullable: false),
                    googlemapsurl = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted = table.Column<bool>(type: "bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<short>(type: "smallint", nullable: false),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted = table.Column<bool>(type: "bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_event",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_event", x => new { x.UserId, x.EventId });
                });

            migrationBuilder.InsertData(
                table: "event",
                columns: new[] { "id", "created_at", "created_by", "deleted", "deleted_at", "deleted_by", "description", "end_date", "googlemapsurl", "location", "name", "start_date", "status", "updated_at", "updated_by" },
                values: new object[] { new Guid("69414953-6494-481b-98e0-99d02d8cd412"), new DateTime(2023, 12, 27, 23, 35, 37, 872, DateTimeKind.Utc).AddTicks(8960), new Guid("e100ec94-d169-41c7-9884-625b11b53e00"), false, null, null, "Eventy is a event management system", new DateTime(2023, 12, 28, 0, 5, 37, 872, DateTimeKind.Utc).AddTicks(8956), "https://g.co/kgs/mxYNbz", "Eventy's office", "Eventy Demo", new DateTime(2023, 12, 27, 23, 35, 37, 872, DateTimeKind.Utc).AddTicks(8955), (short)1, null, null });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "created_at", "created_by", "deleted", "deleted_at", "deleted_by", "email", "name", "password", "role", "status", "updated_at", "updated_by" },
                values: new object[] { new Guid("e100ec94-d169-41c7-9884-625b11b53e00"), new DateTime(2023, 12, 27, 23, 35, 37, 872, DateTimeKind.Utc).AddTicks(6489), new Guid("e100ec94-d169-41c7-9884-625b11b53e00"), false, null, null, "demo@eventy.com", "Demonstration User", "D8JbWxbnXKDiPvF1bG6sfg==|jf2wEVz2XcT+htNw25He0jGS2FnXiYNl60KBI6H5gSs=", (short)1, (short)1, null, null });

            migrationBuilder.InsertData(
                table: "user_event",
                columns: new[] { "EventId", "UserId", "Status" },
                values: new object[] { new Guid("69414953-6494-481b-98e0-99d02d8cd412"), new Guid("e100ec94-d169-41c7-9884-625b11b53e00"), 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "event");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "user_event");
        }
    }
}
