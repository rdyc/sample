using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.Api.Migrations
{
    public partial class Itu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_orders",
                schema: "sample",
                table: "orders");

            migrationBuilder.DropSequence(
                name: "orderseq",
                schema: "sample");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                schema: "sample",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                schema: "sample",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "OrderStatusId",
                schema: "sample",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                schema: "sample",
                table: "orders");

            migrationBuilder.RenameTable(
                name: "orders",
                schema: "sample",
                newName: "persons");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "sample",
                table: "persons",
                newName: "LastName");

            migrationBuilder.CreateSequence(
                name: "personseq",
                schema: "sample",
                incrementBy: 10);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "sample",
                table: "persons",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "sample",
                table: "persons",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_persons",
                schema: "sample",
                table: "persons",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_persons",
                schema: "sample",
                table: "persons");

            migrationBuilder.DropSequence(
                name: "personseq",
                schema: "sample");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "sample",
                table: "persons");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "sample",
                table: "persons");

            migrationBuilder.RenameTable(
                name: "persons",
                schema: "sample",
                newName: "orders");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "sample",
                table: "orders",
                newName: "Description");

            migrationBuilder.CreateSequence(
                name: "orderseq",
                schema: "sample",
                incrementBy: 10);

            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                schema: "sample",
                table: "orders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                schema: "sample",
                table: "orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "OrderStatusId",
                schema: "sample",
                table: "orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                schema: "sample",
                table: "orders",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_orders",
                schema: "sample",
                table: "orders",
                column: "Id");
        }
    }
}
