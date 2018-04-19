using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InterOn.Repo.Migrations
{
    public partial class AddPhotoMainAndSubCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainCategoryPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(maxLength: 255, nullable: false),
                    MainCategoryRef = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCategoryPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainCategoryPhoto_MainCategories_MainCategoryRef",
                        column: x => x.MainCategoryRef,
                        principalTable: "MainCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategoryPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(maxLength: 255, nullable: false),
                    SubCategoryRef = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoryPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategoryPhoto_SubCategories_SubCategoryRef",
                        column: x => x.SubCategoryRef,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MainCategoryPhoto_MainCategoryRef",
                table: "MainCategoryPhoto",
                column: "MainCategoryRef",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryPhoto_SubCategoryRef",
                table: "SubCategoryPhoto",
                column: "SubCategoryRef",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MainCategoryPhoto");

            migrationBuilder.DropTable(
                name: "SubCategoryPhoto");
        }
    }
}
