using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookApplication.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "User_History",
            columns: table => new
            {
                Id = table.Column<Guid>(nullable: false),
                Users_Id = table.Column<Guid>(nullable: false),
                To_Buy = table.Column<string>(nullable: true),
                Owned_Books = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_User_History", x => x.Id);
                table.ForeignKey(
                    name: "FK_User_History_Users_Users_Id",
                    column: x => x.Users_Id,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_History");
        }
    }
}
