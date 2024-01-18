using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class i : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Author = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Text = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Expense_groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense_groups", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Income_groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Income_groups", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Reminder_dayEnum = table.Column<int>(type: "int", nullable: false),
                    Reminder_day = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
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
                    Username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<float>(type: "float", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExpenseGroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Expense_groups_ExpenseGroupId",
                        column: x => x.ExpenseGroupId,
                        principalTable: "Expense_groups",
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
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<float>(type: "float", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IncomeGroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incomes_Income_groups_IncomeGroupId",
                        column: x => x.IncomeGroupId,
                        principalTable: "Income_groups",
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
                table: "Blogs",
                columns: new[] { "Id", "Author", "Created_at", "Description", "Text" },
                values: new object[,]
                {
                    { 1, "http://author1.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(3), "Blog Description 1", "Blog Text 1" },
                    { 2, "http://author2.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(107), "Blog Description 2", "Blog Text 2" },
                    { 3, "http://author3.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(124), "Blog Description 3", "Blog Text 3" },
                    { 4, "http://author4.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(197), "Blog Description 4", "Blog Text 4" },
                    { 5, "http://author5.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(213), "Blog Description 5", "Blog Text 5" },
                    { 6, "http://author6.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(233), "Blog Description 6", "Blog Text 6" },
                    { 7, "http://author7.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(248), "Blog Description 7", "Blog Text 7" },
                    { 8, "http://author8.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(263), "Blog Description 8", "Blog Text 8" },
                    { 9, "http://author9.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(277), "Blog Description 9", "Blog Text 9" },
                    { 10, "http://author10.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(293), "Blog Description 10", "Blog Text 10" },
                    { 11, "http://author11.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(311), "Blog Description 11", "Blog Text 11" },
                    { 12, "http://author12.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(325), "Blog Description 12", "Blog Text 12" },
                    { 13, "http://author13.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(340), "Blog Description 13", "Blog Text 13" },
                    { 14, "http://author14.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(354), "Blog Description 14", "Blog Text 14" },
                    { 15, "http://author15.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(368), "Blog Description 15", "Blog Text 15" },
                    { 16, "http://author16.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(383), "Blog Description 16", "Blog Text 16" },
                    { 17, "http://author17.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(398), "Blog Description 17", "Blog Text 17" },
                    { 18, "http://author18.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(412), "Blog Description 18", "Blog Text 18" },
                    { 19, "http://author19.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(427), "Blog Description 19", "Blog Text 19" },
                    { 20, "http://author20.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(442), "Blog Description 20", "Blog Text 20" },
                    { 21, "http://author21.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(509), "Blog Description 21", "Blog Text 21" },
                    { 22, "http://author22.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(525), "Blog Description 22", "Blog Text 22" },
                    { 23, "http://author23.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(539), "Blog Description 23", "Blog Text 23" },
                    { 24, "http://author24.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(554), "Blog Description 24", "Blog Text 24" },
                    { 25, "http://author25.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(568), "Blog Description 25", "Blog Text 25" },
                    { 26, "http://author26.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(583), "Blog Description 26", "Blog Text 26" },
                    { 27, "http://author27.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(597), "Blog Description 27", "Blog Text 27" },
                    { 28, "http://author28.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(612), "Blog Description 28", "Blog Text 28" },
                    { 29, "http://author29.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(627), "Blog Description 29", "Blog Text 29" },
                    { 30, "http://author30.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(641), "Blog Description 30", "Blog Text 30" },
                    { 31, "http://author31.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(655), "Blog Description 31", "Blog Text 31" },
                    { 32, "http://author32.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(669), "Blog Description 32", "Blog Text 32" },
                    { 33, "http://author33.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(682), "Blog Description 33", "Blog Text 33" },
                    { 34, "http://author34.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(697), "Blog Description 34", "Blog Text 34" },
                    { 35, "http://author35.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(712), "Blog Description 35", "Blog Text 35" },
                    { 36, "http://author36.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(726), "Blog Description 36", "Blog Text 36" },
                    { 37, "http://author37.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(740), "Blog Description 37", "Blog Text 37" },
                    { 38, "http://author38.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(911), "Blog Description 38", "Blog Text 38" },
                    { 39, "http://author39.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(927), "Blog Description 39", "Blog Text 39" },
                    { 40, "http://author40.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(942), "Blog Description 40", "Blog Text 40" },
                    { 41, "http://author41.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(956), "Blog Description 41", "Blog Text 41" },
                    { 42, "http://author42.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(970), "Blog Description 42", "Blog Text 42" },
                    { 43, "http://author43.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(984), "Blog Description 43", "Blog Text 43" },
                    { 44, "http://author44.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(998), "Blog Description 44", "Blog Text 44" },
                    { 45, "http://author45.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1012), "Blog Description 45", "Blog Text 45" },
                    { 46, "http://author46.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1027), "Blog Description 46", "Blog Text 46" },
                    { 47, "http://author47.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1041), "Blog Description 47", "Blog Text 47" },
                    { 48, "http://author48.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1055), "Blog Description 48", "Blog Text 48" },
                    { 49, "http://author49.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1069), "Blog Description 49", "Blog Text 49" },
                    { 50, "http://author50.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1084), "Blog Description 50", "Blog Text 50" },
                    { 51, "http://author51.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1097), "Blog Description 51", "Blog Text 51" },
                    { 52, "http://author52.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1110), "Blog Description 52", "Blog Text 52" },
                    { 53, "http://author53.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1125), "Blog Description 53", "Blog Text 53" },
                    { 54, "http://author54.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1140), "Blog Description 54", "Blog Text 54" },
                    { 55, "http://author55.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1153), "Blog Description 55", "Blog Text 55" },
                    { 56, "http://author56.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1223), "Blog Description 56", "Blog Text 56" },
                    { 57, "http://author57.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1237), "Blog Description 57", "Blog Text 57" },
                    { 58, "http://author58.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1251), "Blog Description 58", "Blog Text 58" },
                    { 59, "http://author59.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1265), "Blog Description 59", "Blog Text 59" },
                    { 60, "http://author60.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1279), "Blog Description 60", "Blog Text 60" },
                    { 61, "http://author61.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1293), "Blog Description 61", "Blog Text 61" },
                    { 62, "http://author62.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1308), "Blog Description 62", "Blog Text 62" },
                    { 63, "http://author63.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1321), "Blog Description 63", "Blog Text 63" },
                    { 64, "http://author64.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1335), "Blog Description 64", "Blog Text 64" },
                    { 65, "http://author65.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1349), "Blog Description 65", "Blog Text 65" },
                    { 66, "http://author66.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1367), "Blog Description 66", "Blog Text 66" },
                    { 67, "http://author67.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1381), "Blog Description 67", "Blog Text 67" },
                    { 68, "http://author68.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1396), "Blog Description 68", "Blog Text 68" },
                    { 69, "http://author69.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1410), "Blog Description 69", "Blog Text 69" },
                    { 70, "http://author70.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1425), "Blog Description 70", "Blog Text 70" },
                    { 71, "http://author71.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1475), "Blog Description 71", "Blog Text 71" },
                    { 72, "http://author72.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1494), "Blog Description 72", "Blog Text 72" },
                    { 73, "http://author73.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1508), "Blog Description 73", "Blog Text 73" },
                    { 74, "http://author74.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1522), "Blog Description 74", "Blog Text 74" },
                    { 75, "http://author75.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1537), "Blog Description 75", "Blog Text 75" },
                    { 76, "http://author76.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1551), "Blog Description 76", "Blog Text 76" },
                    { 77, "http://author77.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1565), "Blog Description 77", "Blog Text 77" },
                    { 78, "http://author78.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1580), "Blog Description 78", "Blog Text 78" },
                    { 79, "http://author79.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1594), "Blog Description 79", "Blog Text 79" },
                    { 80, "http://author80.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1608), "Blog Description 80", "Blog Text 80" },
                    { 81, "http://author81.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1623), "Blog Description 81", "Blog Text 81" },
                    { 82, "http://author82.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1637), "Blog Description 82", "Blog Text 82" },
                    { 83, "http://author83.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1651), "Blog Description 83", "Blog Text 83" },
                    { 84, "http://author84.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1665), "Blog Description 84", "Blog Text 84" },
                    { 85, "http://author85.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1679), "Blog Description 85", "Blog Text 85" },
                    { 86, "http://author86.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1694), "Blog Description 86", "Blog Text 86" },
                    { 87, "http://author87.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1706), "Blog Description 87", "Blog Text 87" },
                    { 88, "http://author88.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1721), "Blog Description 88", "Blog Text 88" },
                    { 89, "http://author89.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1769), "Blog Description 89", "Blog Text 89" },
                    { 90, "http://author90.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1788), "Blog Description 90", "Blog Text 90" },
                    { 91, "http://author91.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1802), "Blog Description 91", "Blog Text 91" },
                    { 92, "http://author92.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1817), "Blog Description 92", "Blog Text 92" },
                    { 93, "http://author93.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1831), "Blog Description 93", "Blog Text 93" },
                    { 94, "http://author94.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1845), "Blog Description 94", "Blog Text 94" },
                    { 95, "http://author95.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1859), "Blog Description 95", "Blog Text 95" },
                    { 96, "http://author96.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1873), "Blog Description 96", "Blog Text 96" },
                    { 97, "http://author97.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1886), "Blog Description 97", "Blog Text 97" },
                    { 98, "http://author98.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1901), "Blog Description 98", "Blog Text 98" },
                    { 99, "http://author99.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1918), "Blog Description 99", "Blog Text 99" },
                    { 100, "http://author100.com", new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1933), "Blog Description 100", "Blog Text 100" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountType", "AccountTypeEnum", "Created_at", "Email", "Password", "Username" },
                values: new object[] { 1, "Regular", 0, new DateTime(2024, 1, 18, 15, 1, 52, 251, DateTimeKind.Local).AddTicks(1963), "admin@gmail.com", "06032004", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseGroupId",
                table: "Expenses",
                column: "ExpenseGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
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
                name: "Expense_groups");

            migrationBuilder.DropTable(
                name: "Income_groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
