using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevRainAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    company = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sentiment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    positiveScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    negativeScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    neutralScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Feedback__3214EC07F31E4D0A", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedback");
        }
    }
}
