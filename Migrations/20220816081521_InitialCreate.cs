using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMIS_API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inst_Regions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    added_user_id = table.Column<int>(type: "int", nullable: false),
                    added_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_user_id = table.Column<int>(type: "int", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inst_Regions", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inst_Regions");
        }
    }
}
