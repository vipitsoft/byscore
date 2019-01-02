using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BYSCORE.API.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applicationlog",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    application = table.Column<string>(maxLength: 50, nullable: false),
                    logged = table.Column<DateTime>(nullable: false),
                    level = table.Column<string>(maxLength: 50, nullable: false),
                    message = table.Column<string>(maxLength: 512, nullable: false),
                    logger = table.Column<string>(maxLength: 250, nullable: true),
                    callsite = table.Column<string>(maxLength: 512, nullable: true),
                    exception = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationlog", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    addtime = table.Column<DateTime>(nullable: false),
                    isdelete = table.Column<bool>(nullable: false),
                    name = table.Column<string>(maxLength: 20, nullable: false),
                    description = table.Column<string>(maxLength: 500, nullable: false),
                    category = table.Column<int>(nullable: false),
                    price = table.Column<decimal>(nullable: false),
                    discout = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicationlog");

            migrationBuilder.DropTable(
                name: "product");
        }
    }
}
