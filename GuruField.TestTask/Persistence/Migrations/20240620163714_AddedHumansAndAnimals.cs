using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedHumansAndAnimals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Humans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MostFavoriteAnimalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeastFavoriteAnimalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Humans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Humans_Animals_LeastFavoriteAnimalId",
                        column: x => x.LeastFavoriteAnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Humans_Animals_MostFavoriteAnimalId",
                        column: x => x.MostFavoriteAnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Predators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PredatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FavoritePreyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Predators_Animals_FavoritePreyId",
                        column: x => x.FavoritePreyId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Predators_Animals_PredatorId",
                        column: x => x.PredatorId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_Name",
                table: "Animals",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Humans_LeastFavoriteAnimalId",
                table: "Humans",
                column: "LeastFavoriteAnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Humans_MostFavoriteAnimalId",
                table: "Humans",
                column: "MostFavoriteAnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Humans_Name",
                table: "Humans",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Predators_FavoritePreyId",
                table: "Predators",
                column: "FavoritePreyId");

            migrationBuilder.CreateIndex(
                name: "IX_Predators_PredatorId",
                table: "Predators",
                column: "PredatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Humans");

            migrationBuilder.DropTable(
                name: "Predators");

            migrationBuilder.DropTable(
                name: "Animals");
        }
    }
}
