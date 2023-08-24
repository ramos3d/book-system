using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookApplication.Migrations
{
	/// <inheritdoc />
	public partial class Firstmigration : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Books",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
					YearPublished = table.Column<int>(type: "int", nullable: false),
					Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Cover = table.Column<string>(type: "nvarchar(max)", nullable: true) 
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Books", x => x.Id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Books");
		}
	}
}
