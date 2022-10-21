using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProcessManagement.Migrations
{
    public partial class @null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperProject_Developers_DevelopersId",
                table: "DeveloperProject");

            migrationBuilder.DropForeignKey(
                name: "FK_Developers_Professions_ProfessionId",
                table: "Developers");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Developers_DeveloperId",
                table: "Missions");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Projects_ProjectId",
                table: "Missions");

            migrationBuilder.RenameColumn(
                name: "DevelopersId",
                table: "DeveloperProject",
                newName: "ProjectId");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Missions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DeveloperId",
                table: "Missions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MissionId",
                table: "Missions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Managers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProfessionId",
                table: "Developers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MissionId",
                table: "Developers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Developers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectId",
                table: "Projects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_MissionId",
                table: "Missions",
                column: "MissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperProject_Developers_ProjectId",
                table: "DeveloperProject",
                column: "ProjectId",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Developers_Professions_ProfessionId",
                table: "Developers",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Developers_DeveloperId",
                table: "Missions",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Developers_MissionId",
                table: "Missions",
                column: "MissionId",
                principalTable: "Developers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Projects_ProjectId",
                table: "Missions",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Managers_ProjectId",
                table: "Projects",
                column: "ProjectId",
                principalTable: "Managers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperProject_Developers_ProjectId",
                table: "DeveloperProject");

            migrationBuilder.DropForeignKey(
                name: "FK_Developers_Professions_ProfessionId",
                table: "Developers");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Developers_DeveloperId",
                table: "Missions");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Developers_MissionId",
                table: "Missions");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Projects_ProjectId",
                table: "Missions");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Managers_ProjectId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Missions_MissionId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "MissionId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "MissionId",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Developers");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "DeveloperProject",
                newName: "DevelopersId");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeveloperId",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProfessionId",
                table: "Developers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperProject_Developers_DevelopersId",
                table: "DeveloperProject",
                column: "DevelopersId",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Developers_Professions_ProfessionId",
                table: "Developers",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Developers_DeveloperId",
                table: "Missions",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Projects_ProjectId",
                table: "Missions",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
