using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReminderDayEnum = table.Column<int>(type: "int", nullable: false),
                    ReminderDay = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountTypeEnum = table.Column<int>(type: "int", nullable: false),
                    AccountType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ResetToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ResetTokenExpiration = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsVerified = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(1500)", maxLength: 1500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Author = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Text = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExpenseGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseGroups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IncomeGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomeGroups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<float>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExpenseGroupId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseGroups_ExpenseGroupId",
                        column: x => x.ExpenseGroupId,
                        principalTable: "ExpenseGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<float>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IncomeGroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incomes_IncomeGroups_IncomeGroupId",
                        column: x => x.IncomeGroupId,
                        principalTable: "IncomeGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incomes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountType", "AccountTypeEnum", "CreatedAt", "Email", "IsVerified", "Password", "ResetToken", "ResetTokenExpiration", "Username" },
                values: new object[] { 1, "Administrator", 1, new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(5363), "admin@gmail.com", false, "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", null, null, "Administrator" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Author", "CreatedAt", "Description", "Text", "UserId" },
                values: new object[,]
                {
                    { 1, "http://author1.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6297), "Blog Description 1", "Blog Text 1", 1 },
                    { 2, "http://author2.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6350), "Blog Description 2", "Blog Text 2", 1 },
                    { 3, "http://author3.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6367), "Blog Description 3", "Blog Text 3", 1 },
                    { 4, "http://author4.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6383), "Blog Description 4", "Blog Text 4", 1 },
                    { 5, "http://author5.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6398), "Blog Description 5", "Blog Text 5", 1 },
                    { 6, "http://author6.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6417), "Blog Description 6", "Blog Text 6", 1 },
                    { 7, "http://author7.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6432), "Blog Description 7", "Blog Text 7", 1 },
                    { 8, "http://author8.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6446), "Blog Description 8", "Blog Text 8", 1 },
                    { 9, "http://author9.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6517), "Blog Description 9", "Blog Text 9", 1 },
                    { 10, "http://author10.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6536), "Blog Description 10", "Blog Text 10", 1 },
                    { 11, "http://author11.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6556), "Blog Description 11", "Blog Text 11", 1 },
                    { 12, "http://author12.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6571), "Blog Description 12", "Blog Text 12", 1 },
                    { 13, "http://author13.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6585), "Blog Description 13", "Blog Text 13", 1 },
                    { 14, "http://author14.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6600), "Blog Description 14", "Blog Text 14", 1 },
                    { 15, "http://author15.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6614), "Blog Description 15", "Blog Text 15", 1 },
                    { 16, "http://author16.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6629), "Blog Description 16", "Blog Text 16", 1 },
                    { 17, "http://author17.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6644), "Blog Description 17", "Blog Text 17", 1 },
                    { 18, "http://author18.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6661), "Blog Description 18", "Blog Text 18", 1 },
                    { 19, "http://author19.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6676), "Blog Description 19", "Blog Text 19", 1 },
                    { 20, "http://author20.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6690), "Blog Description 20", "Blog Text 20", 1 },
                    { 21, "http://author21.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6705), "Blog Description 21", "Blog Text 21", 1 },
                    { 22, "http://author22.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6720), "Blog Description 22", "Blog Text 22", 1 },
                    { 23, "http://author23.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6735), "Blog Description 23", "Blog Text 23", 1 },
                    { 24, "http://author24.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6750), "Blog Description 24", "Blog Text 24", 1 },
                    { 25, "http://author25.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6911), "Blog Description 25", "Blog Text 25", 1 },
                    { 26, "http://author26.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6930), "Blog Description 26", "Blog Text 26", 1 },
                    { 27, "http://author27.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6945), "Blog Description 27", "Blog Text 27", 1 },
                    { 28, "http://author28.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6960), "Blog Description 28", "Blog Text 28", 1 },
                    { 29, "http://author29.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6974), "Blog Description 29", "Blog Text 29", 1 },
                    { 30, "http://author30.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(6989), "Blog Description 30", "Blog Text 30", 1 },
                    { 31, "http://author31.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7003), "Blog Description 31", "Blog Text 31", 1 },
                    { 32, "http://author32.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7017), "Blog Description 32", "Blog Text 32", 1 },
                    { 33, "http://author33.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7032), "Blog Description 33", "Blog Text 33", 1 },
                    { 34, "http://author34.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7048), "Blog Description 34", "Blog Text 34", 1 },
                    { 35, "http://author35.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7063), "Blog Description 35", "Blog Text 35", 1 },
                    { 36, "http://author36.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7078), "Blog Description 36", "Blog Text 36", 1 },
                    { 37, "http://author37.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7093), "Blog Description 37", "Blog Text 37", 1 },
                    { 38, "http://author38.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7107), "Blog Description 38", "Blog Text 38", 1 },
                    { 39, "http://author39.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7121), "Blog Description 39", "Blog Text 39", 1 },
                    { 40, "http://author40.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7136), "Blog Description 40", "Blog Text 40", 1 },
                    { 41, "http://author41.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7196), "Blog Description 41", "Blog Text 41", 1 },
                    { 42, "http://author42.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7216), "Blog Description 42", "Blog Text 42", 1 },
                    { 43, "http://author43.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7231), "Blog Description 43", "Blog Text 43", 1 },
                    { 44, "http://author44.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7246), "Blog Description 44", "Blog Text 44", 1 },
                    { 45, "http://author45.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7260), "Blog Description 45", "Blog Text 45", 1 },
                    { 46, "http://author46.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7275), "Blog Description 46", "Blog Text 46", 1 },
                    { 47, "http://author47.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7290), "Blog Description 47", "Blog Text 47", 1 },
                    { 48, "http://author48.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7305), "Blog Description 48", "Blog Text 48", 1 },
                    { 49, "http://author49.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7319), "Blog Description 49", "Blog Text 49", 1 },
                    { 50, "http://author50.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7334), "Blog Description 50", "Blog Text 50", 1 },
                    { 51, "http://author51.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7348), "Blog Description 51", "Blog Text 51", 1 },
                    { 52, "http://author52.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7364), "Blog Description 52", "Blog Text 52", 1 },
                    { 53, "http://author53.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7378), "Blog Description 53", "Blog Text 53", 1 },
                    { 54, "http://author54.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7393), "Blog Description 54", "Blog Text 54", 1 },
                    { 55, "http://author55.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7407), "Blog Description 55", "Blog Text 55", 1 },
                    { 56, "http://author56.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7422), "Blog Description 56", "Blog Text 56", 1 },
                    { 57, "http://author57.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7437), "Blog Description 57", "Blog Text 57", 1 },
                    { 58, "http://author58.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7451), "Blog Description 58", "Blog Text 58", 1 },
                    { 59, "http://author59.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7502), "Blog Description 59", "Blog Text 59", 1 },
                    { 60, "http://author60.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7519), "Blog Description 60", "Blog Text 60", 1 },
                    { 61, "http://author61.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7534), "Blog Description 61", "Blog Text 61", 1 },
                    { 62, "http://author62.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7548), "Blog Description 62", "Blog Text 62", 1 },
                    { 63, "http://author63.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7563), "Blog Description 63", "Blog Text 63", 1 },
                    { 64, "http://author64.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7577), "Blog Description 64", "Blog Text 64", 1 },
                    { 65, "http://author65.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7593), "Blog Description 65", "Blog Text 65", 1 },
                    { 66, "http://author66.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7610), "Blog Description 66", "Blog Text 66", 1 },
                    { 67, "http://author67.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7626), "Blog Description 67", "Blog Text 67", 1 },
                    { 68, "http://author68.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7641), "Blog Description 68", "Blog Text 68", 1 },
                    { 69, "http://author69.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7656), "Blog Description 69", "Blog Text 69", 1 },
                    { 70, "http://author70.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7671), "Blog Description 70", "Blog Text 70", 1 },
                    { 71, "http://author71.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7685), "Blog Description 71", "Blog Text 71", 1 },
                    { 72, "http://author72.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7700), "Blog Description 72", "Blog Text 72", 1 },
                    { 73, "http://author73.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7715), "Blog Description 73", "Blog Text 73", 1 },
                    { 74, "http://author74.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7730), "Blog Description 74", "Blog Text 74", 1 },
                    { 75, "http://author75.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7780), "Blog Description 75", "Blog Text 75", 1 },
                    { 76, "http://author76.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7796), "Blog Description 76", "Blog Text 76", 1 },
                    { 77, "http://author77.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7811), "Blog Description 77", "Blog Text 77", 1 },
                    { 78, "http://author78.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7826), "Blog Description 78", "Blog Text 78", 1 },
                    { 79, "http://author79.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7841), "Blog Description 79", "Blog Text 79", 1 },
                    { 80, "http://author80.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7855), "Blog Description 80", "Blog Text 80", 1 },
                    { 81, "http://author81.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7869), "Blog Description 81", "Blog Text 81", 1 },
                    { 82, "http://author82.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7884), "Blog Description 82", "Blog Text 82", 1 },
                    { 83, "http://author83.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7898), "Blog Description 83", "Blog Text 83", 1 },
                    { 84, "http://author84.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7913), "Blog Description 84", "Blog Text 84", 1 },
                    { 85, "http://author85.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7928), "Blog Description 85", "Blog Text 85", 1 },
                    { 86, "http://author86.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7942), "Blog Description 86", "Blog Text 86", 1 },
                    { 87, "http://author87.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7957), "Blog Description 87", "Blog Text 87", 1 },
                    { 88, "http://author88.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7972), "Blog Description 88", "Blog Text 88", 1 },
                    { 89, "http://author89.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(7987), "Blog Description 89", "Blog Text 89", 1 },
                    { 90, "http://author90.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(8001), "Blog Description 90", "Blog Text 90", 1 },
                    { 91, "http://author91.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(8017), "Blog Description 91", "Blog Text 91", 1 },
                    { 92, "http://author92.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(8085), "Blog Description 92", "Blog Text 92", 1 },
                    { 93, "http://author93.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(8102), "Blog Description 93", "Blog Text 93", 1 },
                    { 94, "http://author94.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(8117), "Blog Description 94", "Blog Text 94", 1 },
                    { 95, "http://author95.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(8132), "Blog Description 95", "Blog Text 95", 1 },
                    { 96, "http://author96.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(8146), "Blog Description 96", "Blog Text 96", 1 },
                    { 97, "http://author97.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(8161), "Blog Description 97", "Blog Text 97", 1 },
                    { 98, "http://author98.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(8175), "Blog Description 98", "Blog Text 98", 1 },
                    { 99, "http://author99.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(8190), "Blog Description 99", "Blog Text 99", 1 },
                    { 100, "http://author100.com", new DateTime(2024, 2, 7, 15, 8, 41, 391, DateTimeKind.Local).AddTicks(8204), "Blog Description 100", "Blog Text 100", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseGroups_UserId",
                table: "ExpenseGroups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseGroupId",
                table: "Expenses",
                column: "ExpenseGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeGroups_UserId",
                table: "IncomeGroups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_IncomeGroupId",
                table: "Incomes",
                column: "IncomeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_UserId",
                table: "Incomes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "ExpenseGroups");

            migrationBuilder.DropTable(
                name: "IncomeGroups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
