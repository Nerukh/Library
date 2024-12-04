using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConsoleAppLibrarys.Migrations
{
    /// <inheritdoc />
    public partial class Library : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    Genre = table.Column<string>(type: "TEXT", nullable: false),
                    Pages = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Street = table.Column<string>(type: "TEXT", nullable: false),
                    Birthday = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReaderBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReaderId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReaderBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReaderBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReaderBooks_Readers_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "Readers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Genre", "Name", "Pages", "Price" },
                values: new object[,]
                {
                    { 1, "George Orwell", "Dystopian", "1984", 328, 15 },
                    { 2, "Harper Lee", "Fiction", "To Kill a Mockingbird", 281, 10 },
                    { 3, "F. Scott Fitzgerald", "Classic", "The Great Gatsby", 180, 12 }
                });

            migrationBuilder.InsertData(
                table: "Readers",
                columns: new[] { "Id", "Birthday", "Name", "Street" },
                values: new object[,]
                {
                    { 1, new DateTime(1985, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Doe", "123 Maple Street" },
                    { 2, new DateTime(1990, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane Smith", "456 Oak Avenue" },
                    { 3, new DateTime(1978, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alice Johnson", "789 Pine Road" }
                });

            migrationBuilder.InsertData(
                table: "ReaderBooks",
                columns: new[] { "Id", "BookId", "Data", "ReaderId", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0 },
                    { 2, 2, new DateTime(2023, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 3, 3, new DateTime(2023, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReaderBooks_BookId",
                table: "ReaderBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_ReaderBooks_ReaderId",
                table: "ReaderBooks",
                column: "ReaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReaderBooks");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Readers");
        }
    }
}
