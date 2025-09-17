using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cobs.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "base");

            migrationBuilder.CreateTable(
                name: "Wallets",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletBalance = table.Column<decimal>(type: "decimal(18,0)", precision: 18, scale: 0, nullable: false, defaultValue: 0m),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalSchema: "base",
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TransactionAmount = table.Column<decimal>(type: "decimal(18,0)", precision: 18, scale: 0, nullable: false),
                    WalletBalance = table.Column<decimal>(type: "decimal(18,0)", precision: 18, scale: 0, nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalSchema: "base",
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Product = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,0)", precision: 18, scale: 0, nullable: false, defaultValue: 0m),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "base",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,0)", precision: 18, scale: 0, nullable: false, defaultValue: 0m),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "base",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "base",
                table: "Wallets",
                columns: new[] { "Id", "WalletBalance" },
                values: new object[,]
                {
                    { 1, 100m },
                    { 2, 200m }
                });

            migrationBuilder.InsertData(
                schema: "base",
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Role", "WalletId" },
                values: new object[,]
                {
                    { 1, "ali@example.com", "Ali", "Rezaei", 1, 1 },
                    { 2, "sara@example.com", "Sara", "Ahmadi", 2, 2 }
                });

            migrationBuilder.InsertData(
                schema: "base",
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderDate", "Product", "Quantity", "TotalAmount" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Laptop", 1, 1200.75m },
                    { 2, 2, new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phone", 2, 1500.00m }
                });

            migrationBuilder.InsertData(
                schema: "base",
                table: "Invoices",
                columns: new[] { "Id", "Amount", "DueDate", "OrderId", "Status" },
                values: new object[,]
                {
                    { 1, 1200.75m, new DateTime(2025, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, 1500.00m, new DateTime(2025, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                schema: "base",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_WalletId",
                schema: "base",
                table: "Customers",
                column: "WalletId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OrderId",
                schema: "base",
                table: "Invoices",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                schema: "base",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletId",
                schema: "base",
                table: "Transactions",
                column: "WalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Transactions",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "base");

            migrationBuilder.DropTable(
                name: "Wallets",
                schema: "base");
        }
    }
}
