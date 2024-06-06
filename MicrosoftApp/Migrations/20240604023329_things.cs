using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MicrosoftApp.Migrations
{
    /// <inheritdoc />
    public partial class things : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTask_TaskLists_TaskListUserID",
                table: "UserTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTask",
                table: "UserTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskLists",
                table: "TaskLists");

            migrationBuilder.RenameColumn(
                name: "TaskListUserID",
                table: "UserTask",
                newName: "TaskListID");

            migrationBuilder.RenameIndex(
                name: "IX_UserTask_TaskListUserID",
                table: "UserTask",
                newName: "IX_UserTask_TaskListID");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserTask",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "TaskLists",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "TaskListID",
                table: "TaskLists",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTask",
                table: "UserTask",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskLists",
                table: "TaskLists",
                column: "TaskListID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTask_TaskLists_TaskListID",
                table: "UserTask",
                column: "TaskListID",
                principalTable: "TaskLists",
                principalColumn: "TaskListID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTask_TaskLists_TaskListID",
                table: "UserTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTask",
                table: "UserTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskLists",
                table: "TaskLists");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserTask");

            migrationBuilder.DropColumn(
                name: "TaskListID",
                table: "TaskLists");

            migrationBuilder.RenameColumn(
                name: "TaskListID",
                table: "UserTask",
                newName: "TaskListUserID");

            migrationBuilder.RenameIndex(
                name: "IX_UserTask_TaskListID",
                table: "UserTask",
                newName: "IX_UserTask_TaskListUserID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "TaskLists",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTask",
                table: "UserTask",
                column: "ParentList");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskLists",
                table: "TaskLists",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTask_TaskLists_TaskListUserID",
                table: "UserTask",
                column: "TaskListUserID",
                principalTable: "TaskLists",
                principalColumn: "UserID");
        }
    }
}
