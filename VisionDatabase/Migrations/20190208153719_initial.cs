using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VisionDatabase.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Filename = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    EventTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CorrelationId = table.Column<string>(type: "varchar(50)", nullable: true),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false),
                    ImageLocation = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Source = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ExpiryOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageId);
                });

            migrationBuilder.CreateTable(
                name: "Content",
                columns: table => new
                {
                    ContentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ImageId = table.Column<string>(type: "varchar(50)", nullable: false),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    ContentDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.ContentId);
                    table.ForeignKey(
                        name: "FK_Content_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Content_ImageId",
                table: "Content",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_CorrelationId",
                table: "Image",
                column: "CorrelationId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_Filename",
                table: "Image",
                column: "Filename",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_ImageId",
                table: "Image",
                column: "ImageId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Content");

            migrationBuilder.DropTable(
                name: "Image");
        }
    }
}
