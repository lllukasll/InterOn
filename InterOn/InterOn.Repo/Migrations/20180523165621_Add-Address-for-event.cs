using Microsoft.EntityFrameworkCore.Migrations;

namespace InterOn.Repo.Migrations
{
    public partial class AddAddressforevent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Events");
        }
    }
}
