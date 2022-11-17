using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityTodo.Data.Migrations
{
    public partial class AddTodos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Task = table.Column<string>(type: "TEXT", nullable: false),
                    Complete = table.Column<bool>(type: "INTEGER", nullable: false),
                    Owner = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItems");
        }
    }
}
