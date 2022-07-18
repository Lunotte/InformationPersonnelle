using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformationPersonnelle.Server.Migrations
{
    public partial class AjoutParentCategorie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Niveau",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentCategorieId",
                table: "Categories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategorieId",
                table: "Categories",
                column: "ParentCategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategorieId",
                table: "Categories",
                column: "ParentCategorieId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategorieId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentCategorieId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Niveau",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ParentCategorieId",
                table: "Categories");
        }
    }
}
