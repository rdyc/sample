using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sample");

            migrationBuilder.CreateSequence(
                name: "orderseq",
                schema: "sample",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "sample",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    BuyerId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    OrderStatusId = table.Column<int>(nullable: false),
                    PaymentMethodId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                schema: "sample",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders",
                schema: "sample");

            migrationBuilder.DropTable(
                name: "requests",
                schema: "sample");

            migrationBuilder.DropSequence(
                name: "orderseq",
                schema: "sample");
        }
    }
}
