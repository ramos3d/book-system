using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookApplication.Migrations
{
	/// <inheritdoc />
	public partial class CreateUsersAndHistoryTables : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "users",
				columns: table => new
				{
					id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					name = table.Column<string>(nullable: true),
					email = table.Column<string>(nullable: true),
					password = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_users", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "users_history",
				columns: table => new
				{
					id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					users_id = table.Column<int>(nullable: false),
					to_buy = table.Column<string>(nullable: true),
					owned_books = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_users_history", x => x.id);
					table.ForeignKey(
						name: "FK_users_history_users_users_id",
						column: x => x.users_id,
						principalTable: "users",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});
		}


		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "users");
			migrationBuilder.DropTable(
				name: "users_history");
		}
	}
}
