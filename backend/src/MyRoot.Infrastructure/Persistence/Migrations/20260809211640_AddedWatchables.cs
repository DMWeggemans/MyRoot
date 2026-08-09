using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRoot.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedWatchables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Watchables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: true),
                    EpisodeNumber = table.Column<int>(type: "int", nullable: true),
                    SeasonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WatchableMovie_Duration = table.Column<TimeSpan>(type: "time", nullable: true),
                    SeasonNumber = table.Column<int>(type: "int", nullable: true),
                    ShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Watchables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Watchables_Watchables_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Watchables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Watchables_Watchables_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Watchables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Watchables_SeasonId",
                table: "Watchables",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Watchables_ShowId",
                table: "Watchables",
                column: "ShowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Watchables");
        }
    }
}
