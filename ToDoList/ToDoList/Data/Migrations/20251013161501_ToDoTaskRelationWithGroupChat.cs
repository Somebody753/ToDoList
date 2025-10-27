using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Data.Migrations
{
    /// <inheritdoc />
    public partial class ToDoTaskRelationWithGroupChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChatGroupId",
                table: "ToDoTask",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoTask_ChatGroupId",
                table: "ToDoTask",
                column: "ChatGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoTask_ChatGroup_ChatGroupId",
                table: "ToDoTask",
                column: "ChatGroupId",
                principalTable: "ChatGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoTask_ChatGroup_ChatGroupId",
                table: "ToDoTask");

            migrationBuilder.DropIndex(
                name: "IX_ToDoTask_ChatGroupId",
                table: "ToDoTask");

            migrationBuilder.DropColumn(
                name: "ChatGroupId",
                table: "ToDoTask");
        }
    }
}
