using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MicrosoftApp.Migrations
{
    /// <inheritdoc />
    public partial class TaskLists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskLists",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskLists", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "UserTask",
                columns: table => new
                {
                    ParentList = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsImportant = table.Column<bool>(type: "boolean", nullable: false),
                    DueDate = table.Column<string>(type: "text", nullable: false),
                    TaskListUserID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTask", x => x.ParentList);
                    table.ForeignKey(
                        name: "FK_UserTask_TaskLists_TaskListUserID",
                        column: x => x.TaskListUserID,
                        principalTable: "TaskLists",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTask_TaskListUserID",
                table: "UserTask",
                column: "TaskListUserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTask");

            migrationBuilder.DropTable(
                name: "TaskLists");
        }
    }
}
